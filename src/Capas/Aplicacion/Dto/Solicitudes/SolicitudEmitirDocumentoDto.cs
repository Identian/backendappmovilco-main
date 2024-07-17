using Aplicacion.Entidad.Documentos;

namespace Aplicacion.Dto.Solicitudes
{
  public class SolicitudEmitirDocumentoDto
  {
    public FacturaGeneralDto? Factura { get; set; }
    public string? DocumentosAdjuntos { get; set; }
    public string? IdUsuario { get; set; }
    public string? TipoApp { get; set; }
    public string? SerialLogico { get; set; }
  }
}
