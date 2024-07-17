using Dominio.Entidad.Dispositivos;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Transversal.Comun.Respuestas;

namespace Infraestructura.Repositorio
{
  public class GestionAccesoAppMovil : IGestionAccesoAppMovil
  {
    private readonly IConfiguration _configuracion;

    public GestionAccesoAppMovil(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }

    public dynamic? GestionAccesoEmpresa(string Token, string idEmpresa)
    {
      dynamic? respuesta = null;
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:GestionAccesoAppMovil:Url"]!);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:GestionAccesoAppMovil:ApiConsultarAccesoEmpresa"]!.Replace("{idEmpresa}", idEmpresa));
      clienteRest.AddDefaultHeader("Authorization", string.Concat("Bearer ", Token));
      solicitudRest.Method = Method.Get;
      solicitudRest.RequestFormat = DataFormat.Json;
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful && respuestaRest.Content != null)
      {
        respuesta = JsonConvert.DeserializeObject<dynamic>(respuestaRest.Content);
      }
      else if (respuestaRest.ErrorException != null)
      {
        respuesta = new
        {
          Codigo = Convert.ToInt32(respuestaRest.StatusCode),
          Mensaje = string.IsNullOrEmpty(respuestaRest.ErrorMessage) ? respuestaRest.ErrorException.Message : respuestaRest.ErrorMessage,
          Resultado = "Error"
        };
      }
      return respuesta;
    }

    public dynamic? GestionAccesoUsuario(string Token, string idEmpresa, string idUsuario)
    {
      dynamic? respuesta = null;
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:GestionAccesoAppMovil:Url"]!);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:GestionAccesoAppMovil:ApiConsultarAccesoUsuario"]!
      .Replace("{idEmpresa}", idEmpresa)
      .Replace("{idUsuario}", idUsuario));
      clienteRest.AddDefaultHeader("Authorization", string.Concat("Bearer ", Token));
      solicitudRest.Method = Method.Get;
      solicitudRest.RequestFormat = DataFormat.Json;
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful && respuestaRest.Content != null)
      {
        respuesta = JsonConvert.DeserializeObject<dynamic>(respuestaRest.Content);
      }
      else if (respuestaRest.ErrorException != null)
      {
        respuesta = new
        {
          Codigo = Convert.ToInt32(respuestaRest.StatusCode),
          Mensaje = string.IsNullOrEmpty(respuestaRest.ErrorMessage) ? respuestaRest.ErrorException.Message : respuestaRest.ErrorMessage,
          Resultado = "Error"
        };
      }
      return respuesta;
    }

    public JToken? CrearAccesoDispositivo(Dispositivo solicitud, string Token)
    {
      JToken respuesta;
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:GestionAccesoAppMovil:Url"]!);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:GestionAccesoAppMovil:ApiCrearAccesoDispositivo"]!)
      {
        Method = Method.Post,
        RequestFormat = DataFormat.Json
      };
      clienteRest.AddDefaultHeader("Authorization", string.Concat("Bearer ", Token));
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
      return respuesta;
    }

    public JToken? ConsultarSuscripcionDispositivo(string idEmpresa, string idSuscripcion, string token)
    {
      JToken respuesta;
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:GestionAccesoAppMovil:Url"]!);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:GestionAccesoAppMovil:ApiConsultarSuscripcionDispositivo"]!
        .Replace("{idEmpresa}", idEmpresa)
        .Replace("{idSuscripcion}", idSuscripcion))
      {
        Method = Method.Get
      };
      clienteRest.AddDefaultHeader("Authorization", string.Concat("Bearer ", token));
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful)
      {
        respuesta = JToken.Parse(respuestaRest.Content!);
      }
      else
      {
        respuesta = JToken.FromObject(RespuestaBase.Error500("Infraestructura", string.IsNullOrEmpty(respuestaRest.ErrorMessage) ? respuestaRest.ErrorException!.Message : respuestaRest.ErrorMessage));
      }
      return respuesta;
    }

    public JToken? AsociarAlias(SolicitudAsociarAlias solicitud, string token)
    {
      JToken respuesta;
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:GestionAccesoAppMovil:Url"]!);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:GestionAccesoAppMovil:AsociarAlias"]!)
      {
        Method = Method.Put,
        RequestFormat = DataFormat.Json
      };
      clienteRest.AddDefaultHeader("Authorization", string.Concat("Bearer ", token));
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
      return respuesta;
    }
  }
}
