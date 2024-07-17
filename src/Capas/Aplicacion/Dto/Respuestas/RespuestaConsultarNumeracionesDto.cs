namespace Aplicacion.Dto.Respuestas
{
  public class RespuestaConsultarNumeracionesDto
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public string? Plataforma { get; set; }
    public IEnumerable<NumeracionFacturacionDto>? Numeraciones { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
