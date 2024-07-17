using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Infraestructura.Repositorio
{
  public class DeliveryRepositorio : IDeliveryRepositorio
  {
    private readonly IConfiguration _configuracion;

    public DeliveryRepositorio(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }

    public RespuestaEnviarCorreoIndividual EnviarCorreoIndividual(SolicitudEnviarCorreoIndividual solicitud, string bearerToken, string valorBearerToken)
    {
      RespuestaEnviarCorreoIndividual respuesta = new();

      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:Delivery:Url"]);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:Delivery:ApiEnviarCorreoIndividual"]);
      clienteRest.AddDefaultHeader("Authorization", bearerToken);
      solicitudRest.Method = Method.Post;
      solicitudRest.RequestFormat = DataFormat.Json;
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      solicitudRest.AddJsonBody(JsonConvert.SerializeObject(solicitud));
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if ((respuestaRest.IsSuccessful) && !string.IsNullOrEmpty(respuestaRest.Content))
      {
        respuesta = JsonConvert.DeserializeObject<RespuestaEnviarCorreoIndividual>(respuestaRest.Content)!;
      }
      else
      {
        respuesta.Codigo = Convert.ToInt32(respuestaRest.StatusCode);
        respuesta.Mensaje = string.IsNullOrEmpty(respuestaRest.ErrorMessage) ? respuestaRest.ErrorException!.Message : respuestaRest.ErrorMessage;
        respuesta.Resultado = "Error";
      }

      return (respuesta);
    }
  }
}
