using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Transversal.Comun.Respuestas;

namespace Infraestructura.Repositorio
{
  public class ReportesRepositorio : IReportesRepositorio
  {
    private readonly IConfiguration _configuracion;

    public ReportesRepositorio(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }

    public RespuestaReporteEnLinea ReporteEnLinea(SolicitudReporteEnLinea solicitudReporteEnLinea, string bearerToken)
    {
      RespuestaReporteEnLinea respuesta = new();

      var clienteRest = new RestClient(ObtenerUrlServicio(solicitudReporteEnLinea.Sistema));
      var solicitudRest = new RestRequest(ObtenerApiReporteEnLinea(solicitudReporteEnLinea.Sistema));
      clienteRest.AddDefaultHeader("Authorization", bearerToken);
      solicitudRest.Method = Method.Post;
      solicitudRest.RequestFormat = DataFormat.Json;
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      solicitudRest.AddJsonBody(JsonConvert.SerializeObject(solicitudReporteEnLinea));
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful)
      {
        respuesta = JsonConvert.DeserializeObject<RespuestaReporteEnLinea>(respuestaRest.Content);
      }
      else
      {
        respuesta.Codigo = Convert.ToInt32(respuestaRest.StatusCode);
        respuesta.Mensaje = string.IsNullOrEmpty(respuestaRest.ErrorMessage) ? respuestaRest.ErrorException.Message : respuestaRest.ErrorMessage;
        respuesta.Resultado = "Error";
      }
      return respuesta;
    }

    private string ObtenerNombreServicio(string sistema)
    {
      switch (sistema)
      {
        case "2":
          return "ServiciosRecepcion";
        case "3":
          return "ServiciosNomina";
        default:
          return "ServiciosFacturacion";
      }
    }

    public string ObtenerUrlServicio(string sistema)
    {
      return _configuracion[string.Concat(ObtenerNombreServicio(sistema), ":ReportesRest:Url")]!;
    }

    public string ObtenerApiReporteEnLinea(string sistema)
    {
      return _configuracion[string.Concat(ObtenerNombreServicio(sistema), ":ReportesRest:ReporteEnLinea:Api")]!;
    }
    public string ObtenerCodigoReporteGeneralEnLinea(string sistema)
    {
      return _configuracion[string.Concat(ObtenerNombreServicio(sistema), ":ReportesRest:ReporteEnLinea:CodigoReporteGeneral")]!;
    }     
     
  }
}
