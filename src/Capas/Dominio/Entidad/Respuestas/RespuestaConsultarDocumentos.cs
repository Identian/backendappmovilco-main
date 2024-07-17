using Newtonsoft.Json.Linq;

namespace Dominio.Entidad.Respuestas
{
  public class RespuestaConsultarDocumentos
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public int TotalDocumentos { get; set; }
    public JToken? Documentos { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
