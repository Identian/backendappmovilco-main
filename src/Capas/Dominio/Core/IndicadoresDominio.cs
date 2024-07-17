using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Dominio.Entidad.Solicitudes.FiltrosSolicitudes;
using Dominio.Interfaz;
using Infraestructura.Interfaz;
using Newtonsoft.Json.Linq;
using Transversal.Comun.Respuestas;

namespace Dominio.Core
{
  public class IndicadoresDominio : IIndicadoresDominio
  {
    private readonly IRedisCacheRepositorio _redisCacheRepositorio;
    private readonly IEmpresaAutenticacionRepositorio _empresaAutenticacionRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio;
    private readonly IIndicadoresRepositorio _indicadoresRepositorio;

    public IndicadoresDominio(IRedisCacheRepositorio redisCacheRepositorio, IEmpresaAutenticacionRepositorio empresaAutenticacionRepositorio, IEmpresaRepositorio empresaRepositorio, IIndicadoresRepositorio indicadoresRepositorio)
    {
      _redisCacheRepositorio = redisCacheRepositorio;
      _empresaAutenticacionRepositorio = empresaAutenticacionRepositorio;
      _indicadoresRepositorio = indicadoresRepositorio;
      _empresaRepositorio = empresaRepositorio;
    }

    public JToken ConsultarTotalDocumentos(FiltrosTotalDocumentos filtros, string bearerToken, string valorBearerToken)
    {
      JToken respuesta;
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
            return JToken.FromObject(RespuestaBase.Error401("Dominio", "Datos Token inválidos"));
          }

        }
      }
      if ((usuarioEnCache != null) && (usuarioEnCache.Codigo == 200))
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
        var empresa = _empresaRepositorio.ConsultarEmpresaPorId(Convert.ToInt32(datosToken.IdEmpresa));
        SolicitudConsultarTotalDocumentos solicitud = new()
        {
          Nit = empresa.Contribuyente!.NumeroIdentificacion,
          TokenEmpresa = empresa.Contribuyente!.TokenEmpresa,
          TokenClave = empresa.Contribuyente!.TokenClave,
          Filtros = filtros
        };
        respuesta = _indicadoresRepositorio.ConsultarTotalDocumentos(solicitud);
      }
      else
      {
        RespuestaBase respuestaConError = new()
        {
          Codigo = 401,
          Resultado = "Error",
          Mensaje = "Se ha cerrado la sesión del usuario"
        };
        respuesta = JToken.FromObject(respuestaConError);
      }
      return (respuesta);
    }
  }
}
