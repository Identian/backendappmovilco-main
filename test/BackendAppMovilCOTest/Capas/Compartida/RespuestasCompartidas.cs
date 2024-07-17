using Aplicacion.Dto.Respuestas;
using Dominio.Entidad;
using Dominio.Entidad.Respuestas;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using Transversal.Comun.Respuestas;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidas
  {
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionExitosa { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionProcesadoConErrores { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionExitosaActivoAppUsuarioInactivo { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioRolEstandarAutenticacionExitosa { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAppEmpresaInvalido { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAppInvalido { get; set; }
    public RespuestaIniciarSesion? RespuestaEmpresaNoEncontradaGestionAccesoApp { get; set; }
    public RespuestaIniciarSesion? RespuestausuarioNoEncontrad0GestionAccesoApp { get; set; }
    public RespuestaIniciarSesionDto? RespuestaUsuarioRolEstandarAutenticacionExitosaDto { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionExitosaUsuarioNoEncontradoEnBD { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionUsuarioNoEncontradoEnCache { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionUsuarioInvalido { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionUsuarioInactivo { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionContrasenaInvalida { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionEmpresaInactiva { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionEmpresaUsuarioInactivo { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionTokensInvalidos { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioRolEstandarAutenticacionExitosaActivoAppInvalido { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionTimeOutCache { get; set; }
    public RespuestaIniciarSesion? RespuestaUsuarioAutenticacionTimeOutCacheDatosTokenInvalido { get; set; }
    public RespuestaBase? RespuestaUsuarioAutenticacionTokenExpiradoEnCache { get; set; }

    public const string responseAutenticacionExitosa = "success";
    public const string responseAutenticacionprocesadoConErrores = "Procesado con errores";
    public const string responseAutenticacionError = "error";
    public const string messageAutenticacionExitosa = "Inicio de Sesión exitoso.";
    public const string messageAutenticacionProcesadoConErrores = "No se pudo insertar la información sobre el inicio de sesión en la Redis Caché";
    public const string messageAutenticacionUsuarioNoEncontradoEnCache = "Usuario no encontrado en caché";
    public const string messageAutenticacionUsuarioInvalido = "Usuario inválido";
    public const string messageAutenticacionUsuarioInactivo = "Usuario inactivo";
    public const string messageAutenticacionContrasenaInvalida = "Contraseña inválida";
    public const string messageAutenticacionEmpresaInactiva = "Empresa inactiva";
    public const string messageAutenticacionEmpresaUsuarioInactivo = "Empresa usuario inactivo";
    public const string messageAutenticacionTokensInvalidos = "Tokens inválidos";
    public const string messageAccesoAppEmpresaInvalido = "Empresa no tiene activado permiso de acceso a la App";
    public const string messageAccesoAppUsuarioInvalido = "Usuario no tiene activado permiso de acceso a la App";
    public const string messageAccesoAppUsuarioEmpresaNoEncontrada = "Registro no encontrado para la empresa con Id: 439";
    public const string messageSesionExpirada = "No autorizado. Capa de Dominio";

    private void InicializarRespuestasUsuarioAutenticacion()
    {
      RespuestaUsuarioAutenticacionExitosa = new()
      {
        Codigo = 200,
        response = responseAutenticacionExitosa,
        message = messageAutenticacionExitosa,
        token = ConstantesCompartidasFacturacion.BearerTokenValido,
        Usuario = new()
        {
          Id = ConstantesCompartidasFacturacion.IdUsuario,
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioExistente,
          Nombre = "Nombre Usuario",
          Apellido = null,
          NombreUsuario = null,
          Administrador = "1",
          Roles = new List<RollUsuarioFacturacion>()
          {
            new()
            {
              Codigo = "23",
              Tipo = "user-advanced",
              Descripcion = "Acceso para agregar y editar registros, ajustes y configuraciones."
            }
          },
          Activo = 1
        },
        Contribuyente = JToken.FromObject(new
        {
          NumeroIdentificacion = ConstantesCompartidasFacturacion.NitExistente,
          TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
          TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido,
          ClaveSecreta = ConstantesCompartidasFacturacion.ClaveSecretaValida
        })
      };

      RespuestaUsuarioAutenticacionProcesadoConErrores = new()
      {
        Codigo = 200,
        response = responseAutenticacionprocesadoConErrores,
        message = messageAutenticacionProcesadoConErrores,
        token = ConstantesCompartidasFacturacion.BearerTokenValido,
        Usuario = new()
        {
          Id = ConstantesCompartidasFacturacion.IdUsuarioAutenticadoConErrores,
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioExistente,
          Nombre = "Nombre Usuario",
          Apellido = null,
          NombreUsuario = null,
          Administrador = "1",
          Roles = new List<RollUsuarioFacturacion>()
          {
            new()
            {
              Codigo = "23",
              Tipo = "user-advanced",
              Descripcion = "Acceso para agregar y editar registros, ajustes y configuraciones."
            }
          },
          Activo = 1
        },
        Contribuyente = JToken.FromObject(new
        {
          NumeroIdentificacion = ConstantesCompartidasFacturacion.NitExistente,
          TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
          TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido,
          ClaveSecreta = ConstantesCompartidasFacturacion.ClaveSecretaValida
        })
      };



      RespuestaUsuarioRolEstandarAutenticacionExitosa = new()
      {
        Codigo = 200,
        response = responseAutenticacionExitosa,
        message = messageAutenticacionExitosa,
        token = ConstantesCompartidasFacturacion.BearerTokenValido,
        Usuario = new()
        {
          Id = ConstantesCompartidasFacturacion.IdUsuarioRolEstandar,
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar,
          Nombre = "Usuario Estandar",
          Apellido = null,
          NombreUsuario = null,
          Administrador = "0",
          Roles = new List<RollUsuarioFacturacion>()
          {
            new()
            {
              Codigo = "22",
              Tipo = "user-standar",
              Descripcion = "Acceso básicos con opciones limitadas para agregar y visualizar sin afectar configuraciones."
            }
          },
          Activo = 1
        },
        Contribuyente = JToken.FromObject(new
        {
          NumeroIdentificacion = ConstantesCompartidasFacturacion.NitExistente,
          TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
          TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido
        })
      };

      RespuestaUsuarioRolEstandarAutenticacionExitosaActivoAppInvalido = new()
      {
        Codigo = 200,
        response = responseAutenticacionExitosa,
        message = messageAutenticacionExitosa,
        token = ConstantesCompartidasFacturacion.BearerTokenValidoActivoAppInvalido,
        Usuario = new()
        {
          Id = ConstantesCompartidasFacturacion.IdUsuarioRolEstandar,
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar,
          Nombre = "Usuario Estandar",
          Apellido = null,
          NombreUsuario = null,
          Administrador = "0",
          Roles = new List<RollUsuarioFacturacion>()
          {
            new()
            {
              Codigo = "22",
              Tipo = "user-standar",
              Descripcion = "Acceso básicos con opciones limitadas para agregar y visualizar sin afectar configuraciones."
            }
          },
          Activo = 1
        },
        Contribuyente = JToken.FromObject(new
        {
          NumeroIdentificacion = ConstantesCompartidasFacturacion.NitExistente,
          TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
          TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido
        })
      };

      RespuestaEmpresaNoEncontradaGestionAccesoApp = new()
      {
        Codigo = 404,
        response = responseAutenticacionError,
        message = messageAccesoAppUsuarioEmpresaNoEncontrada
      };

      RespuestausuarioNoEncontrad0GestionAccesoApp = new()
      {
        Codigo = 404,
        response = responseAutenticacionError,
        message = messageAccesoAppUsuarioEmpresaNoEncontrada
      };

      RespuestaUsuarioRolEstandarAutenticacionExitosaDto = new()
      {
        Codigo = 200,
        response = responseAutenticacionExitosa,
        message = messageAutenticacionExitosa,
        token = ConstantesCompartidasFacturacion.BearerTokenValido,
        Usuario = new()
        {
          Id = ConstantesCompartidasFacturacion.IdUsuarioRolEstandar,
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar,
          Nombre = "Usuario Estandar",
          Apellido = null,
          NombreUsuario = null,
          Administrador = "0",
          Roles = new List<RollUsuarioFacturacion>()
          {
            new()
            {
              Codigo = "22",
              Tipo = "user-standar",
              Descripcion = "Acceso básicos con opciones limitadas para agregar y visualizar sin afectar configuraciones."
            }
          },
          Activo = "1"
        },
        Contribuyente = JToken.FromObject(new
        {
          NumeroIdentificacion = ConstantesCompartidasFacturacion.NitExistente,
          TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
          TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido
        })
      };

      RespuestaUsuarioAutenticacionExitosaUsuarioNoEncontradoEnBD = new()
      {
        Codigo = 200,
        response = responseAutenticacionExitosa,
        message = messageAutenticacionExitosa,
        token = ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado,
        Usuario = new()
        {
          Id = "456",
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioInexistente,
          Nombre = "Nombre Usuario",
          Apellido = null,
          NombreUsuario = null,
          Activo = 1
        }
      };

      RespuestaUsuarioAutenticacionUsuarioNoEncontradoEnCache = new()
      {
        Codigo = 404,
        response = responseAutenticacionError,
        message = messageAutenticacionUsuarioNoEncontradoEnCache
      };

      RespuestaUsuarioAutenticacionTokenExpiradoEnCache = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoSessionCerrada,
        Resultado = "Error",
        Mensaje = "No autorizado. Capa de Dominio",
        Errores = new List<String>() {
        "Se ha cerrado la sesión del usuario"
        }
      };

      RespuestaUsuarioAutenticacionTimeOutCache = new()
      {
        Codigo = 504,
        response = "Error",
        message = "Se supero el tiempo de conexion a la redis cache",
        Errores = new List<String>() {
          ConstantesCompartidasFacturacion.TimeoutRedisCache
        }
      };

      RespuestaUsuarioAutenticacionUsuarioInvalido = new()
      {
        response = responseAutenticacionError,
        message = messageAutenticacionUsuarioInvalido
      };

      RespuestaUsuarioAutenticacionUsuarioInactivo = new()
      {
        response = responseAutenticacionError,
        message = messageAutenticacionUsuarioInactivo
      };

      RespuestaUsuarioAutenticacionContrasenaInvalida = new()
      {
        response = responseAutenticacionError,
        message = messageAutenticacionContrasenaInvalida
      };

      RespuestaUsuarioAutenticacionEmpresaInactiva = new()
      {
        response = responseAutenticacionError,
        message = messageAutenticacionEmpresaInactiva
      };

      RespuestaUsuarioAutenticacionEmpresaUsuarioInactivo = new()
      {
        response = responseAutenticacionError,
        message = messageAutenticacionEmpresaUsuarioInactivo
      };

      RespuestaUsuarioAutenticacionTokensInvalidos = new()
      {
        response = responseAutenticacionError,
        message = messageAutenticacionTokensInvalidos
      };

      RespuestaUsuarioAppEmpresaInvalido = new()
      {
        Codigo = 401,
        response = responseAutenticacionError,
        message = messageAccesoAppEmpresaInvalido
      };

      RespuestaUsuarioAppInvalido = new()
      {
        Codigo = 401,
        response = responseAutenticacionError,
        message = messageAccesoAppUsuarioInvalido
      };

      RespuestaEmpresaNoEncontradaGestionAccesoApp = new()
      {
        Codigo = 404,
        response = responseAutenticacionError,
        message = messageAccesoAppUsuarioInvalido
      };
    }

    public RespuestasCompartidas()
    {
      InicializarRespuestasUsuarioAutenticacion();
    }

    public static dynamic DatosValidosControlAccesoEmpresa()
    {
      dynamic respuesta = new ExpandoObject();
      respuesta.Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos;
      respuesta.Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos;
      respuesta.Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos;
      // Crear objeto dinámico para AccesoContribuyente
      dynamic accesoContribuyente = new ExpandoObject();
      accesoContribuyente.ActivoApp = ConstantesCompartidasFacturacion.UsuarioActivoApp;
      accesoContribuyente.ActivoTili = ConstantesCompartidasFacturacion.UsuarioActivoApp;
      respuesta.AccesoContribuyente = accesoContribuyente;
      return respuesta;
    }

    public static dynamic DatosInvalidosControlAccesoEmpresa()
    {
      dynamic respuesta = new ExpandoObject();
      respuesta.Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos;
      respuesta.Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos;
      respuesta.Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos;
      // Crear objeto dinámico para AccesoContribuyente
      dynamic accesoContribuyente = new ExpandoObject();
      accesoContribuyente.ActivoApp = ConstantesCompartidasFacturacion.UsuarioInactivoApp;
      accesoContribuyente.ActivoTili = ConstantesCompartidasFacturacion.UsuarioInactivoApp;
      respuesta.AccesoContribuyente = accesoContribuyente;
      return respuesta;
    }

    public static dynamic DatosInvalidosNoExistenRegistrosControlAccesoEmpresa()
    {
      dynamic respuesta = new ExpandoObject();
      respuesta.Codigo = ConstantesCompartidasFacturacion.CodigoNitNoExiste;
      respuesta.Resultado = ConstantesCompartidasFacturacion.ResultadoDatosInvalidos;
      respuesta.Mensaje = messageAccesoAppUsuarioEmpresaNoEncontrada;
      return respuesta;
    }

    public static dynamic DatosValidosControlAccesoUsuario()
    {
      dynamic respuesta = new ExpandoObject();
      respuesta.Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos;
      respuesta.Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos;
      respuesta.Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos;
      respuesta.AccesoUsuario = new List<dynamic>();
      dynamic accesoUsuarioElemento = new ExpandoObject();
      accesoUsuarioElemento.IdUsuario = ConstantesCompartidasFacturacion.IdUsuario;
      accesoUsuarioElemento.Activo = ConstantesCompartidasFacturacion.UsuarioActivo;
      accesoUsuarioElemento.ActivoApp = ConstantesCompartidasFacturacion.UsuarioActivoApp;
      accesoUsuarioElemento.ActivoTili = ConstantesCompartidasFacturacion.UsuarioActivoApp;
      // Agregar el elemento a la lista AccesoUsuario
      respuesta.AccesoUsuario.Add(accesoUsuarioElemento);
      return respuesta;
    }

    public static dynamic DatosInvalidosControlAccesoUsuario()
    {
      dynamic respuesta = new ExpandoObject();
      respuesta.Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos;
      respuesta.Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos;
      respuesta.Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos;
      respuesta.AccesoUsuario = new List<dynamic>();
      dynamic accesoUsuarioElemento = new ExpandoObject();
      accesoUsuarioElemento.IdUsuario = ConstantesCompartidasFacturacion.IdUsuario;
      accesoUsuarioElemento.Activo = ConstantesCompartidasFacturacion.UsuarioActivo;
      accesoUsuarioElemento.ActivoApp = ConstantesCompartidasFacturacion.UsuarioInactivoApp;
      accesoUsuarioElemento.ActivoTili = ConstantesCompartidasFacturacion.UsuarioInactivoApp;
      // Agregar el elemento a la lista AccesoUsuario
      respuesta.AccesoUsuario.Add(accesoUsuarioElemento);
      return respuesta;
    }

    public static dynamic DatosValidosAutenticacionUsuarioFacturacion()
    {
      dynamic respuesta = new ExpandoObject();
      respuesta.Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos;
      respuesta.Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos;
      respuesta.Mensaje = "Autenticación exitosa";
      respuesta.Token = ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil;
      return respuesta;
    }
  }
}
