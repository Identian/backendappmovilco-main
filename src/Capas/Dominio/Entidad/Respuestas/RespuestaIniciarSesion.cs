using Newtonsoft.Json.Linq;

namespace Dominio.Entidad.Respuestas
{
  public class RespuestaIniciarSesion
  {
    public int Codigo { get; set; }
    public string? response { get; set; }
    public string? message { get; set; }
    public UsuarioFacturacion? Usuario { get; set; }
    public JToken? Contribuyente { get; set; }
    public string? token { get; set; }
    public string? passwordExpiration { get; set; }
    public TimeSpan? TiempoExpiracion { get; set; }
    public List<string>? Errores { get; set; }
  }
}
