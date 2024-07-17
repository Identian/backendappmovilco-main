using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Infraestructura.Repositorio
{
  public class DocumentosRepositorio : IDocumentosRepositorio
  {
    private readonly IConfiguration _configuracion;

    public DocumentosRepositorio(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }

    public RespuestaEmitirDocumento EmitirDocumento(SolicitudEmitirDocumento solicitudEmitirDocumento, string bearerToken)
    {
      RespuestaEmitirDocumento? respuesta = new();
      string tipoApp;

      if (solicitudEmitirDocumento.TipoApp == "1")
      {
        tipoApp = "AppMovil";
      }
      else
      {
        tipoApp = "AppTili";
      }

      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:EmisionRest:Url"]!);
      var solicitudRest = new RestRequest(_configuracion[string.Concat("ServiciosFacturacion:EmisionRest:ApiEnviar:", tipoApp)]);

      clienteRest.AddDefaultHeader("Authorization", bearerToken);
      solicitudRest.Method = Method.Post;
      solicitudRest.RequestFormat = DataFormat.Json;
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      solicitudRest.AddJsonBody(JsonConvert.SerializeObject(solicitudEmitirDocumento));
      var respuestaRest = clienteRest.Execute(solicitudRest)!;
      if (respuestaRest.IsSuccessful)
      {
        respuesta = JsonConvert.DeserializeObject<RespuestaEmitirDocumento>(respuestaRest.Content!);
      }
      else
      {
        respuesta.Codigo = Convert.ToInt32(respuestaRest.StatusCode);
        respuesta.Mensaje = string.IsNullOrEmpty(respuestaRest.ErrorMessage) ? respuestaRest.ErrorException!.Message : respuestaRest.ErrorMessage;
        respuesta.Resultado = "Error";
      }
      return (respuesta!);
    }
  }
}
