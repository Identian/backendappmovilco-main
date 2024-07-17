using Newtonsoft.Json.Linq;

namespace Aplicacion.Dto.Respuestas
{
  public class RespuestaIniciarSesionDto
  {
    public int Codigo { get; set; }
    public string? response { get; set; }
    public string? message { get; set; }
    public UsuarioFacturacionDto? Usuario { get; set; }
    public JToken? Contribuyente { get; set; }
    public string? token { get; set; }
    public string? passwordExpiration { get; set; }
  }
}
