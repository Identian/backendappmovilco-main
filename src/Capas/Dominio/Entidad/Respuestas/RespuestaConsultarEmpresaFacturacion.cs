
using Dominio.Entidad.Empresas;

namespace Dominio.Entidad.Respuestas
{
  public class RespuestaConsultarEmpresaFacturacion
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public string? Plataforma { get; set; }
    public EmpresaFacturacion? Contribuyente { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
