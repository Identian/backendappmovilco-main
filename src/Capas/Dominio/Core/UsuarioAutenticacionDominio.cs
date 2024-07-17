using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Infraestructura.Interfaz;

namespace Dominio.Core
{
  public class UsuarioAutenticacionDominio : IUsuarioAutenticacionDominio
  {
    private readonly IRedisCacheRepositorio _redisCacheRepositorio;
    private readonly IUsuarioAutenticacionRepositorio _usuarioAutenticacionRepositorio;
    private readonly IEmpresaAutenticacionRepositorio _empresaAutenticacionRepositorio;
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ICifradoRepositorio _cifradoRepositorio;
    private readonly IAutenticacionGestionAccesoAppMovil _autenticacionGestionAccesoAppMovil;
    private readonly IGestionAccesoAppMovil _gestionAccesoAppMovil;
 

    public UsuarioAutenticacionDominio(IRedisCacheRepositorio redisCacheRepositorio, IUsuarioAutenticacionRepositorio usuarioAutenticacionRepositorio,
           IEmpresaAutenticacionRepositorio empresaAutenticacionRepositorio, IUsuarioRepositorio usuarioRepositorio, ICifradoRepositorio cifradoRepositorio, IAutenticacionGestionAccesoAppMovil utenticacionGestionAccesoAppMovil, IGestionAccesoAppMovil gestionAccesoAppMovil)
    {
      _redisCacheRepositorio = redisCacheRepositorio;
      _usuarioAutenticacionRepositorio = usuarioAutenticacionRepositorio;
      _empresaAutenticacionRepositorio = empresaAutenticacionRepositorio;
      _usuarioRepositorio = usuarioRepositorio;
      _cifradoRepositorio = cifradoRepositorio;
      _autenticacionGestionAccesoAppMovil = utenticacionGestionAccesoAppMovil;
      _gestionAccesoAppMovil = gestionAccesoAppMovil;
    }

    public RespuestaIniciarSesion AutenticarUsuario(UsuarioAutenticacion solicitud)
    {
      solicitud.Contrasena = _cifradoRepositorio.CifrarTexto(solicitud.Contrasena!);
      var respuestaAutenticacion = _usuarioAutenticacionRepositorio.AutenticarUsuario(solicitud);
      RespuestaIniciarSesion respuesta = new();

      if (respuestaAutenticacion.Codigo != 200)
      {
        respuesta = respuestaAutenticacion;
        return respuesta; // Devuelve la respuesta en caso de que las credenciales sean erroneas
      }

      //Consultar información de Usuario
      var valorToken = _cifradoRepositorio.DecodificarJwtToken(respuestaAutenticacion.token!);
      var respuestaToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorToken);
      var respuestaInformacion = _usuarioRepositorio.ConsultarInformacion(respuestaToken.IdEmpresa!, respuestaToken.NitEmpresa!, solicitud.Usuario!);

      if (respuestaInformacion.Codigo != 200 || respuestaInformacion.Usuario == null)
      {
        respuesta.Codigo = respuestaInformacion.Codigo;
        respuesta.response = respuestaInformacion.Resultado;
        respuesta.message = respuestaInformacion.Mensaje;
        return respuesta; // devuelve la respuesta en caso de error
      }

      //autenticamos al usuario que va a realizar la consulta del activoApp de las empresas
      var atenticarGestionAccesoAppMovil = _autenticacionGestionAccesoAppMovil.AutenticarUsuarioGestionAppMovil()!;

      // si el usuario es invalido o no inicia sesion devolvemos la respuesta
      if (atenticarGestionAccesoAppMovil.Codigo != 200)
      {
        respuesta.Codigo = atenticarGestionAccesoAppMovil.Codigo;
        respuesta.response = atenticarGestionAccesoAppMovil.Resultado;
        respuesta.message = atenticarGestionAccesoAppMovil.Mensaje + " UsuarioGestionAccesoAppMovi";
        return respuesta; // devuelve la respuesta en caso de error
      }

      string tokenUsuarioGestionAppMovil = atenticarGestionAccesoAppMovil.Token;

      //llamamos a la api para consultar las empresas
      var validarAccesoEmpresa = _gestionAccesoAppMovil.GestionAccesoEmpresa(tokenUsuarioGestionAppMovil, respuestaToken.IdEmpresa!)!;

      //si no se encuentra la empresa
      if (validarAccesoEmpresa.Codigo != 200)
      {
        respuesta.Codigo = validarAccesoEmpresa.Codigo;
        respuesta.response = validarAccesoEmpresa.Resultado;
        respuesta.message = "Empresa no encontrada en AccesoAppMovilEmpresa";
        return respuesta;
      }

      var empresaActivoApp = validarAccesoEmpresa.AccesoContribuyente.ActivoApp;
      var empresaActivoTiliApp = validarAccesoEmpresa.AccesoContribuyente.ActivoTili;

      //validamos si la empresa tiene el activoApp = 0 si es asi devolvemos la respuesta 
      if ((validarAccesoEmpresa.Codigo == 200 && empresaActivoApp == 0 && solicitud.TipoApp == "1") || (validarAccesoEmpresa.Codigo == 200 && empresaActivoTiliApp == 0 && solicitud.TipoApp == "2"))
      {
        respuesta.Codigo = 401;
        respuesta.response = "error";
        respuesta.message = "Empresa no tiene activado permiso de acceso a la App";
        return respuesta;
      }

      //llamamos a la api para consultar  los usuarios 
      var validarAccesoUsuario = _gestionAccesoAppMovil.GestionAccesoUsuario(tokenUsuarioGestionAppMovil, respuestaToken.IdEmpresa!, respuestaInformacion.Usuario.Id!)!;

      //si no se eencuentra el usuario
      if (validarAccesoUsuario.Codigo != 200)
      {
        respuesta.Codigo = validarAccesoUsuario.Codigo;
        respuesta.response = validarAccesoUsuario.Resultado;
        respuesta.message = "Usuario no encontrado en AccesoAppMovilUsuario";
        return respuesta;
      }

      var usuarioActivo = validarAccesoUsuario.AccesoUsuario[0].Activo;
      var usuarioActivoApp = validarAccesoUsuario.AccesoUsuario[0].ActivoApp;
      var usuarioActivoTiliApp = validarAccesoUsuario.AccesoUsuario[0].ActivoTili;


      //verificar si el usuario esta activo
      if (usuarioActivo != 1)
      {
        respuesta.Codigo = 401;
        respuesta.response = "error";
        respuesta.message = "Usuario inactivo en AccesoAppMovilUsuario";
        return respuesta;
      }

      //si la empresa tiene ActivoApp = 1 pero el usuario tiene ActivoApp = 0 devolvemos la denegacion
      if ((empresaActivoApp == 1 && usuarioActivoApp == 0 && solicitud.TipoApp == "1") || (empresaActivoTiliApp == 1 && usuarioActivoTiliApp == 0 && solicitud.TipoApp == "2"))
      {
        respuesta.Codigo = 401;
        respuesta.response = "error";
        respuesta.message = "Usuario no tiene activado permiso de acceso a la App";
        return respuesta;
      }

      //llamamos los metodos para construir el inicio de sesion si la empresa y el usuario tienen ActivoApp = 1
      if ((empresaActivoApp == 1 && usuarioActivoApp == 1 && solicitud.TipoApp == "1") || (empresaActivoTiliApp == 1 && usuarioActivoTiliApp == 1 && solicitud.TipoApp == "2"))
      {
        respuestaAutenticacion.Usuario = respuestaInformacion.Usuario;
        var idUsuario = respuestaAutenticacion.Usuario.Id!;
        respuestaAutenticacion.Usuario.Roles = _usuarioRepositorio.ConsultarRollUsuario(idUsuario).rollUsuario;
        respuestaAutenticacion.Contribuyente = _empresaAutenticacionRepositorio.ConsultarClavesEmpresaPorIdUsuario(respuestaToken.IdEmpresa!, idUsuario).ClavesEmpresa;
        respuesta = respuestaAutenticacion; // aqui se actualizan todos los datos y se iguala a respuesta para retornar los datos
      }
      // Guardar en caché
      _redisCacheRepositorio.InsertarUsuarioAutenticado(respuesta);

      return respuesta;
    }


    public RespuestaCerrarSesion CerrarSesion(string bearerToken)
    {
      //Eliminar de la caché
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken
      };
      var usuarioEnCache = _redisCacheRepositorio.ConsultarUsuarioAutenticado(inicioSesion);
      if (usuarioEnCache.Codigo == 200)
      {
        _redisCacheRepositorio.EliminarUsuarioAutenticado(inicioSesion);
      }
      RespuestaCerrarSesion respuesta = new()
      {
        Codigo = 200,
        type = "Se ha cerrado su sesión correctamente.",
        message = "success",
        Usuario = (usuarioEnCache.Codigo == 200 && usuarioEnCache.Usuario != null) ? usuarioEnCache.Usuario.Correo : null
      };
      return (respuesta);
    }
  }
}