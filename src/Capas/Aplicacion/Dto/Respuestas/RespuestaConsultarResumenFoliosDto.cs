namespace Aplicacion.Dto.Respuestas
{
  public class RespuestaConsultarResumenFoliosDto
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public ResumenFoliosDto? Folios { get; set; }
    public List<string>? Errores { get; set; }
  }
}
