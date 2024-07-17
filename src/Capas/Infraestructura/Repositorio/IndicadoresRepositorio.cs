using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Transversal.Comun.Respuestas;

namespace Infraestructura.Repositorio
{
  public class IndicadoresRepositorio : IIndicadoresRepositorio
  {
    private readonly IConfiguration _configuracion;

    public IndicadoresRepositorio(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }

    public JToken ConsultarTotalDocumentos(SolicitudConsultarTotalDocumentos solicitud)
    {
      JToken respuesta;
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:IndicadoresRest:Url"]!);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:IndicadoresRest:ConsultarTotalDocumentos:Api"])
      {
        Method = Method.Post,
        RequestFormat = DataFormat.Json
      };
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      solicitudRest.AddJsonBody(JsonConvert.SerializeObject(solicitud));
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful)
      {
        respuesta = JToken.Parse(respuestaRest.Content!);
      }
      else
      {
        respuesta = JToken.FromObject(RespuestaBase.Error500("Infraestructura", string.IsNullOrEmpty(respuestaRest.ErrorMessage) ? respuestaRest.ErrorException!.Message : respuestaRest.ErrorMessage));
      }
      return (respuesta);
    }
  }
}
