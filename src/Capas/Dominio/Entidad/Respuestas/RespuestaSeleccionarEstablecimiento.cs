
using Dominio.Entidad.EstadoDocumento;
using Newtonsoft.Json.Linq;

namespace Dominio.Entidad.Respuestas
{
  public class RespuestaSeleccionarEstablecimiento
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public string? IdEstablecimiento { get; set; }
    public IEnumerable<string>? Errores { get; set; }

  }
}
