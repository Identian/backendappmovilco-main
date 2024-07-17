using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Infraestructura.Interfaz;
using Newtonsoft.Json.Linq;

namespace Dominio.Core
{
  public class EstablecimientosDominio : IEstablecimientosDominio
  {
    private readonly IEstablecimientosRepositorio _establecimientoRepositorio;
    private readonly IEmpresaAutenticacionRepositorio _empresaAutenticacionRepositorio;
    private readonly IRedisCacheRepositorio _redisCacheRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio;


    public EstablecimientosDominio(IEstablecimientosRepositorio establecimientoRepositorio, IEmpresaAutenticacionRepositorio empresaAutenticacionRepositorio, IRedisCacheRepositorio redisCacheRepositorio, IEmpresaRepositorio empresaRepositorio)
    {

      _establecimientoRepositorio = establecimientoRepositorio;
      _empresaAutenticacionRepositorio = empresaAutenticacionRepositorio;
      _redisCacheRepositorio = redisCacheRepositorio;
      _empresaRepositorio = empresaRepositorio;
    }

    public RespuestaSeleccionarEstablecimiento Seleccionar(SolicitudSeleccionarEstablecimiento solicitud, string bearerToken, string valorBearerToken)
    {
      RespuestaSeleccionarEstablecimiento respuesta = new();
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken
      };
      var usuarioEnCache = _redisCacheRepositorio.ConsultarUsuarioAutenticado(inicioSesion);
      if (usuarioEnCache.Codigo == 504)
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
        var datosEmpresa = _empresaRepositorio.ConsultarEmpresaPorId(Convert.ToInt32(datosToken.IdEmpresa));
        if (datosEmpresa.Codigo == 200)
        {
          var datosContribuyente = datosEmpresa.Contribuyente!;
          if (datosContribuyente.IdEmpresa == datosToken.IdEmpresa! && datosContribuyente.NumeroIdentificacion == datosToken.NitEmpresa && datosContribuyente.TokenEmpresa == datosToken.TokenEmpresa)
          {
            usuarioEnCache.Codigo = datosEmpresa.Codigo;
            usuarioEnCache.Contribuyente = JToken.FromObject(new
            {
              IdEmpresa = datosContribuyente.IdEmpresa,
              NitEmpresa = datosContribuyente.NumeroIdentificacion,
              TokenEmpresa = datosContribuyente.TokenEmpresa,
              TokenClave = datosContribuyente.TokenClave
            });
          }
          else
          {
            respuesta.Codigo = 401;
            respuesta.Resultado = "Error";
            respuesta.Mensaje = "Datos Token inválidos";
            return respuesta;
          }

        }
      }
      if ((usuarioEnCache != null) && (usuarioEnCache.Codigo == 200))
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);

        //llamamos a la api para crear el dispositivo
        respuesta = _establecimientoRepositorio.Seleccionar(solicitud, datosToken);
      }
      else
      {
        respuesta.Codigo = 401;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "Se ha cerrado la sesión del usuario";
      }
      return (respuesta);
    }

  }
}
