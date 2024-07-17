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
  public class ReferenciaDocumentosRepositorio : IReferenciaDocumentosRepositorio
  {
    private readonly IConfiguration _configuracion;

    public ReferenciaDocumentosRepositorio(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }


    public RespuestaConsultarReferenciaDocumento ConsultarRefereciaDocumentoFactura(SolicitudConsultarReferenciaDocumentoFacturacion solicitudConsultarDocumento, string bearerToken)
    {
      RespuestaConsultarReferenciaDocumento respuesta = new();
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:EmisionRest:Url"]!);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:EmisionRest:ApiConsultarDocumentoReferencia"]!.Replace("{id_invoice}", solicitudConsultarDocumento.IdInvoice).Replace("{tipo_consulta}", solicitudConsultarDocumento.TipoConsulta));
      clienteRest.AddDefaultHeader("Authorization", String.Concat("Bearer ", bearerToken));
      solicitudRest.Method = Method.Get;
      solicitudRest.RequestFormat = DataFormat.Json;
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful)
      {

        var cadenaInicial = respuestaRest.Content!.Replace("\\", "").Replace("\"{", "{").Replace("}\"", "}");
        var objetoJson = JObject.Parse(cadenaInicial);
        var cadenaJson = objetoJson.ToString();
        respuesta = JsonConvert.DeserializeObject<RespuestaConsultarReferenciaDocumento>(cadenaJson)!;
        if (respuesta != null && respuesta.Codigo == 200)
        {
          respuesta = JsonConvert.DeserializeObject<RespuestaConsultarReferenciaDocumento>(cadenaJson)!;
        }
        else
        {
          respuesta!.Codigo = respuesta.Codigo;
          respuesta!.Message = respuesta.Message;
          respuesta!.Resultado = respuesta.Resultado;
        }
      }
      else
      {
        respuesta.Codigo = Convert.ToInt32(respuestaRest.StatusCode);
        respuesta.Message = string.IsNullOrEmpty(respuestaRest.ErrorMessage) ? respuestaRest.ErrorException!.Message : respuestaRest.ErrorMessage;
        respuesta.Resultado = "Error";
      }

        return respuesta;
    }
  }
}
