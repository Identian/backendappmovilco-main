
using Dominio.Entidad.EstadoDocumento;
using Newtonsoft.Json.Linq;

namespace Dominio.Entidad.Respuestas
{
  public class RespuestaConsultarEstadoDocumento
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public string? Plataforma { get; set; }
    public EstadoDocumentoFacturacion? Documento { get; set; }
    public IEnumerable<string>? Errores { get; set; }

  }
}
