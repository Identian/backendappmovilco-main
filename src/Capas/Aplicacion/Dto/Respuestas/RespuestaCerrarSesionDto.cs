namespace Aplicacion.Dto.Respuestas
{
  public class RespuestaCerrarSesionDto
  {
    public int Codigo { get; set; }
    public string? type { get; set; }
    public string? message { get; set; }
    public string? Usuario { get; set; }
  }
}
