namespace Aplicacion.Dto.Respuestas
{
  public class RespuestaConsultarUsuarioFacturacionDto
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public UsuarioFacturacionDto? Usuario { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
