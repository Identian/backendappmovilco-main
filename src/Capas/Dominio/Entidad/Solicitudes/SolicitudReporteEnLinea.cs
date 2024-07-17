using Dominio.Entidad.Reportes;

namespace Dominio.Entidad.Solicitudes
{
  public class SolicitudReporteEnLinea
  {
    public string? CantidadDecimales { get; set; }
    public string? UserId { get; set; }
    public string? Siguiente { get; set; }
    public string? FormatoRequerido { get; set; }
    public string? Sistema { get; set; }
    public string? EstadoLegal { get; set; }
    public Filtros? Filtros { get; set; }
  }
}
