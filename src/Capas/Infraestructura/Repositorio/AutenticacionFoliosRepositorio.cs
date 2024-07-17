using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Infraestructura.Repositorio
{
  public class AutenticacionFoliosRepositorio : IAutenticacionFoliosRepositorio
  {
    private readonly IConfiguration _configuracion;

    public AutenticacionFoliosRepositorio(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }
    
    public RespuestaUsuarioAutenticacionFolios AutenticarUsuarioFolios()
    {
      RespuestaUsuarioAutenticacionFolios respuesta = new();
      SolicitudUsuarioAutenticacionFolios solicitud = new();
      solicitud.Usuario = _configuracion["ServiciosFacturacion:AutenticacionFolios:Usuario"];
      solicitud.Contrasena = _configuracion["ServiciosFacturacion:AutenticacionFolios:Clave"];
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:AutenticacionFolios:Url"]!);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:AutenticacionFolios:ApiAutenticar"]);
      solicitudRest.Method = Method.Post;
      solicitudRest.RequestFormat = DataFormat.Json;
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      solicitudRest.AddJsonBody(JsonConvert.SerializeObject(solicitud));
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful)
      {
        respuesta = JsonConvert.DeserializeObject<RespuestaUsuarioAutenticacionFolios>(respuestaRest.Content!)!;
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
