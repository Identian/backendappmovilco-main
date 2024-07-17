namespace Dominio.Entidad.Reportes
{
  public class AgendarReporteProgramado
  {
    public string? CantidadDecimales { get; set; }
    public string? Correo { get; set; }
    public string? EstadoLegal { get; set; }
    public Filtros? Filtros { get; set; }
  }
}
