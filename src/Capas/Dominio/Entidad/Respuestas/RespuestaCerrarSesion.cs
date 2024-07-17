namespace Dominio.Entidad.Respuestas
{
  public class RespuestaCerrarSesion
  {
    public int Codigo { get; set; }
    public string? type { get; set; }
    public string? message { get; set; }
    public string? Usuario { get; set; }
  }
}
