namespace Dominio.Entidad.Respuestas
{
  public class RespuestaConsultarResumenFolios
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public ResumenFolios? Folios { get; set; }
    public List<string>? Errores { get; set; }
  }
}
