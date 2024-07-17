using Dominio.Entidad.EstadoDocumento;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Infraestructura.Repositorio
{
  public class EstadoDocumentosRepositorio : IEstadoDocumentoRepositorio
  {
    private readonly IConfiguration _configuracion;

    public EstadoDocumentosRepositorio(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }

   
    public RespuestaConsultarEstadoDocumento ConsultarDocumentoPorConsecutivoFactura(SolicitudConsultarEstadoDocumentoFacturacion solicitudConsultarDocumento, string bearerToken)
    {
      RespuestaConsultarEstadoDocumento respuesta = new();
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:EmisionRest:Url"]);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:EmisionRest:ApiConsultarDocumentoPorConsecutivo"].Replace("{consecutivoDocumento}", solicitudConsultarDocumento.Consecutivo));
      clienteRest.AddDefaultHeader("Authorization", String.Concat("Bearer ", bearerToken));
      solicitudRest.Method = Method.Get;
      solicitudRest.RequestFormat = DataFormat.Json;
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful)
      {
        respuesta.Documento = JsonConvert.DeserializeObject<EstadoDocumentoFacturacion>(respuestaRest.Content);
        if (respuesta != null && respuesta.Documento!.Codigo == 200)
        {
          respuesta.Documento = JsonConvert.DeserializeObject<EstadoDocumentoFacturacion>(respuestaRest.Content);
        }
        else
        {
          respuesta!.Codigo = respuesta.Documento!.Codigo;
          respuesta.Mensaje = respuesta.Documento.Mensaje;
          respuesta.Resultado = respuesta.Documento.Resultado;
        }
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
