using Aplicacion.Dto.Solicitudes.FiltrosSolicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;
using Aplicacion.Interfaz;
using AutoMapper;
using Dominio.Entidad.Solicitudes.FiltrosSolicitudes;
using Dominio.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Transversal.Comun.Respuestas;

namespace Aplicacion.Principal
{
  public class IndicadoresAplicacion : IIndicadoresAplicacion
  {
    private readonly IConfiguration _configuracion;
    private readonly IIndicadoresDominio _indicadoresDominio;
    private readonly IMapper _mapeador;

    public IndicadoresAplicacion(IConfiguration configuracion, IIndicadoresDominio indicadoresDominio, IMapper mapeador)
    {
      _configuracion = configuracion;
      _indicadoresDominio = indicadoresDominio;
      _mapeador = mapeador;
    }

    public JToken ConsultarTotalDocumentos(FiltrosTotalDocumentosDto filtrosDto, string bearerToken, string valorBearerToken)
    {
      JToken respuestaDto;
      try
      {
        #region Validaciones
        var validaciones = new FiltrosTotalDocumentosDtoValidator(_configuracion);
        var resultadoValidaciones = validaciones.Validate(filtrosDto);
        if (!resultadoValidaciones.IsValid)
        {
          var errores = new List<string>();
          foreach (var error in resultadoValidaciones.Errors)
          {
            errores.Add(error.ErrorMessage);
          }
          RespuestaBase respuestaDtoConErrores = new()
          {
            Codigo = 400,
            Resultado = "Error",
            Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones",
            Errores = errores
          };
          return (JToken.FromObject(respuestaDtoConErrores));
        }
        #endregion Validaciones
        var filtros = _mapeador.Map<FiltrosTotalDocumentos>(filtrosDto);
        respuestaDto = _indicadoresDominio.ConsultarTotalDocumentos(filtros, bearerToken, valorBearerToken);
      }
      catch (Exception ex)
      {
        respuestaDto = JToken.FromObject(RespuestaBase.Error500("Aplicación", ex.Message));
      }
      return (respuestaDto);
    }
  }
}
