using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Infraestructura.Interfaz;
using Newtonsoft.Json.Linq;

namespace Dominio.Core
{
  public class SecuencialesDominio : ISecuencialesDominio
  {
    private readonly ISecuencialesRepositorio _secuencialesRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio;
    private readonly IRedisCacheRepositorio _redisCacheRepositorio;
    private readonly IEmpresaAutenticacionRepositorio _empresaAutenticacionRepositorio;
    public SecuencialesDominio(ISecuencialesRepositorio secuencialesRepositorio, IEmpresaRepositorio empresaRepositorio, IRedisCacheRepositorio redisCacheRepositorio, IEmpresaAutenticacionRepositorio empresaAutenticacionRepositorio)
    {
      _secuencialesRepositorio = secuencialesRepositorio;
      _empresaRepositorio = empresaRepositorio;
      _redisCacheRepositorio = redisCacheRepositorio;
      _empresaAutenticacionRepositorio = empresaAutenticacionRepositorio;
    }
    public RespuestaConsultarNumeraciones ConsultarNumeraciones(SolicitudConsultarFacturacion solicitudConsultarNumeraciones)
    {
      var consultaEmpresa = _empresaRepositorio.ConsultarEmpresaPorId(Convert.ToInt32(solicitudConsultarNumeraciones.IdEmpresa));

      if (consultaEmpresa != null && consultaEmpresa.Contribuyente != null)
      {
        solicitudConsultarNumeraciones.TokenClave = consultaEmpresa.Contribuyente.TokenClave;
      }

      return _secuencialesRepositorio.ConsultarNumeraciones(solicitudConsultarNumeraciones);
    }

    public RespuestaSeleccionarSecuencial Seleccionar(SolicitudSeleccionarSecuencial solicitud, string bearerToken, string valorBearerToken)
    {
      RespuestaSeleccionarSecuencial respuesta = new();
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

        respuesta = _secuencialesRepositorio.Seleccionar(solicitud, datosToken);
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
