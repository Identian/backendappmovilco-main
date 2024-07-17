namespace Dominio.Entidad.Respuestas
{
  public class RespuestaValidarClaveSecreta
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? IdEmpresa { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
