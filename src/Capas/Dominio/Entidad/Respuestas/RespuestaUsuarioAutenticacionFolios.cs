namespace Dominio.Entidad.Respuestas
{
  public class RespuestaUsuarioAutenticacionFolios
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Token { get; set; }
    public string? TiempoExpiracion { get; set; }
    public List<string>? Errores { get; set; }
  }
}
