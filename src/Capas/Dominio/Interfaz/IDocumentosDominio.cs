using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Dominio.Interfaz
{
  public interface IDocumentosDominio
  {
    public RespuestaEmitirDocumento EmitirDocumento(SolicitudEmitirDocumento solicitudEmitirDocumento, string bearerToken, string valorBearerToken);
    public string FormatoFechaHoraRespuestas(DateTime fechaHora);
    public string FormatoFechaHoraSolicitudes(DateTime fechaHora);
    public RespuestaConsultarDocumentos ConsultarDocumentos(SolicitudReporteEnLinea solicitudReporteEnLinea, string bearerToken, string valorBearerToken);
    public RespuestaConsultarEstadoDocumento ConsultarDocumentoPorConsecutivoFactura(SolicitudConsultarEstadoDocumentoFacturacion solicitudConsultarDocumento, string bearerToken, string valorBearerToken);
    public RespuestaEnviarCorreoIndividual EnviarCorreoIndividual(SolicitudEnviarCorreoIndividual solicitud, string bearerToken, string valorBearerToken);
    public RespuestaConsultarReferenciaDocumento ConsultarReferenciaDocumentosFactura(SolicitudConsultarReferenciaDocumentoFacturacion solicitudConsultarDocumento, string bearerToken, string valorBearerToken);
    public RespuestaConsultarMontoFacturaPos ConsultarMontoFacturaPos(string bearerToken, string valorBearerToken);
  }
}
