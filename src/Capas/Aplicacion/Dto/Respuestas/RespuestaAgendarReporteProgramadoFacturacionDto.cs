namespace Dominio.Entidad.Respuestas
{
  public class RespuestaAgendarReporteProgramadoFacturacionDto
  {
    public int Codigo { get; set; }
    public string? Mensaje { get; set; }
    public string? Resultado { get; set; }
    public string? IdReporte { get; set; }
    public string? EstadoLegal { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
