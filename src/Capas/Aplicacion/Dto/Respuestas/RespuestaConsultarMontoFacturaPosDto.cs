namespace Aplicacion.Dto.Respuestas
{
  public class RespuestaConsultarMontoFacturaPosDto
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? MontoUVT { get; set; }
    public string? CantidadUVT { get; set; }
    public string? MontoFacturaPos { get; set; }
    public string? FechaVigencia { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
