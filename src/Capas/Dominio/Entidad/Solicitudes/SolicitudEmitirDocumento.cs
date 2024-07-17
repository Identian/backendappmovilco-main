using Dominio.Entidad.Documentos;

namespace Dominio.Entidad.Solicitudes
{
  public class SolicitudEmitirDocumento
  {
    public FacturaGeneral? Factura { get; set; }
    public string? DocumentosAdjuntos { get; set; }
    public string? IdUsuario { get; set; }
    public string? TipoApp { get; set; }
    public string? SerialLogico { get; set; }
    public string? IdSuscripcion { get; set; }

  }
}
