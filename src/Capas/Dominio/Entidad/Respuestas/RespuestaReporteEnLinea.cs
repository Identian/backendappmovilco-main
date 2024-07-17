using Dominio.Entidad.Reportes;

namespace Dominio.Entidad.Respuestas
{
  public class RespuestaReporteEnLinea
  {
    public int Codigo { get; set; }
    public string? Mensaje { get; set; }
    public string? Resultado { get; set; }
    public string? Siguiente { get; set; }
    public string? TrackId { get; set; }
    public string? Formato { get; set; }
    public string? EstadoLegal { get; set; }
    public string? ReporteCodificado { get; set; }
    public string? Crc { get; set; }
    public List<ReporteBasico>? Reporte { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
