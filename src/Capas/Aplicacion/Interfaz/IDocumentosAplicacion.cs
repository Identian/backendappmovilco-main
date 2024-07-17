using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;
using Aplicacion.Entidad.Respuestas;
using Aplicacion.Entidad.Solicitudes;

namespace Aplicacion.Interfaz
{
  public interface IDocumentosAplicacion
  {
    public RespuestaEmitirDocumentoDto EmitirDocumento(SolicitudEmitirDocumentoDto solicitudEmitirDocumentoDto, string bearerToken, string valorBearerToken);
    public RespuestaConsultarDocumentosDto ConsultarDocumentos(SolicitudConsultarDocumentosDto solicitudConsultarDocumentosDto, string bearerToken, string valorBearerToken);
    public RespuestaConsultarEstadoDocumentoDto ConsultarDocumentoPorConsecutivoFactura(SolicitudConsultarEstadoDocumentoFacturacionDto solicitudConsultarDocumentosDto, string bearerToken, string valorBearerToken);
    public RespuestaEnviarCorreoIndividualDto EnviarCorreoIndividual(SolicitudEnviarCorreoIndividualDto solicitudDto, string bearerToken, string valorBearerToken);
    public RespuestaConsultarReferenciaDocumentoDto ConsultarReferenciaDocumentoFactura(SolicitudConsultarReferenciaDocumentoFacturacionDto solicitudConsultarDocumentosDto, string bearerToken, string valorBearerToken);
    public RespuestaConsultarMontoFacturaPosDto? ConsultarMontoFacturaPos(string bearerToken, string valorBearerToken);
  }
}
