using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Infraestructura.Repositorio
{
  public class AutenticacionGestionAccesoAppMovil : IAutenticacionGestionAccesoAppMovil
  {
    private readonly IConfiguration _configuracion;

    public AutenticacionGestionAccesoAppMovil(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }

    public dynamic? AutenticarUsuarioGestionAppMovil()
    {
      dynamic? respuesta = null;
      JObject? solicitud = new()
      {
        ["Usuario"] = _configuracion["ServiciosFacturacion:AutenticacionGestionAccesoAppMovil:Usuario"],
        ["Contrasena"] = _configuracion["ServiciosFacturacion:AutenticacionGestionAccesoAppMovil:Clave"]
      };

      var clienteRest = !string.IsNullOrEmpty(_configuracion["ServiciosFacturacion:AutenticacionGestionAccesoAppMovil:Url"])
                        ? new RestClient(_configuracion["ServiciosFacturacion:AutenticacionGestionAccesoAppMovil:Url"]!)
                        : throw new InvalidOperationException("La URL del servicio no está configurada correctamente.");

      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:AutenticacionGestionAccesoAppMovil:ApiAutenticar"])
      {
        Method = Method.Post,
        RequestFormat = DataFormat.Json
      };
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      solicitudRest.AddJsonBody(JsonConvert.SerializeObject(solicitud));
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful && respuestaRest.Content != null)
      {
        respuesta = JsonConvert.DeserializeObject<dynamic>(respuestaRest.Content);
      }
      else
      {
        respuesta = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(new
        {
          Codigo = Convert.ToInt32(respuestaRest.StatusCode),
          Mensaje = respuestaRest.ErrorMessage,
          Resultado = "Error"
        }));
      }
      return respuesta;
    }
  }
}
