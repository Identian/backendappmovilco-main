namespace Dominio.Entidad.Respuestas
{
  public class RespuestaValidarCredenciales
  {
    public bool Resultado { get; set; }
    public string? Mensaje { get; set; }
    public int IdUsuario { get; set; }
  }
}
