using Newtonsoft.Json.Linq;

namespace Aplicacion.Dto.Respuestas
{
  public class RespuestaConsultarCatalogoDto
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public IEnumerable<JToken?>? Catalogos { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
