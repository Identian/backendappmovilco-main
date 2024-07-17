namespace Dominio.Entidad.Respuestas
{
  public class RespuestaConsultarRollUsuario
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? IdUsuario { get; set; }
    public List<RollUsuarioFacturacion>? rollUsuario { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
