using Newtonsoft.Json.Linq;

namespace Dominio.Entidad.Respuestas
{
  public class RespuestaConsultarCatalogo
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public IEnumerable<JToken?>? Catalogos { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
