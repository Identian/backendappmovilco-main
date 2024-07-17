using Dominio.Entidad.Respuestas;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Infraestructura.Repositorio
{
  public class FoliosRepositorio : IFoliosRepositorio
  {
    private readonly IConfiguration _configuracion;

    public FoliosRepositorio(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }
    public RespuestaConsultarResumenFolios ConsultarResumen(string idEmpresa,string token)
    {
      RespuestaConsultarResumenFolios respuesta = new();
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:Folios:Url"]);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:Folios:ApiConsultarResumen"].Replace("{idEmpresa}", idEmpresa));
      clienteRest.AddDefaultHeader("Authorization",String.Concat("Bearer ",token) );
      solicitudRest.Method = Method.Get;
      solicitudRest.RequestFormat = DataFormat.Json;
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful)
      {
        respuesta = JsonConvert.DeserializeObject<RespuestaConsultarResumenFolios>(respuestaRest.Content);
      }
      else
      {
        respuesta.Codigo = Convert.ToInt32(respuestaRest.StatusCode);
        respuesta.Mensaje = string.IsNullOrEmpty(respuestaRest.ErrorMessage) ? respuestaRest.ErrorException.Message : respuestaRest.ErrorMessage;
        respuesta.Resultado = "Error";
      }
      return respuesta;
    }
  }
}

