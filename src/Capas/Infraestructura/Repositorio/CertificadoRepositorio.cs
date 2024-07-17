using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Infraestructura.Repositorio
{
  public class CertificadoRepositorio : ICertificadoRepositorio
  {
    private readonly IConfiguration _configuracion;

    public CertificadoRepositorio(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }

    public RespuestaConsultarCertificadoFacturacion Consultar(SolicitudConsultarFacturacion solicitud)
    {
      RespuestaConsultarCertificadoFacturacion respuesta = new();
      var certificadoRest = new RestClient(_configuracion["ServiciosFacturacion:GestionContribuyentes:Url"]!);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:GestionContribuyentes:ApiConsultarCertificado"])
      {
        Method = Method.Post,
        RequestFormat = DataFormat.Json
      };
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      solicitudRest.AddJsonBody(JsonConvert.SerializeObject(solicitud));
      var respuestaRest = certificadoRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful)
      {
        respuesta = JsonConvert.DeserializeObject<RespuestaConsultarCertificadoFacturacion>(respuestaRest.Content!)!;
        if (respuesta.Codigo == 200)
        {
          respuesta = JsonConvert.DeserializeObject<RespuestaConsultarCertificadoFacturacion>(respuestaRest.Content!)!;
        }
        else
        {
          respuesta.Resultado = "Error";
        }
      }
      else
      {
        respuesta.Codigo = Convert.ToInt32(respuestaRest.StatusCode);
        respuesta.Mensaje = string.IsNullOrEmpty(respuestaRest.ErrorMessage) ? respuestaRest.ErrorException!.Message : respuestaRest.ErrorMessage;
        respuesta.Resultado = "Error";
      }
      return respuesta;
    }
  }
}
