using Dominio.Entidad.Documentos;
using Dominio.Entidad.ReferenciaDocumento;

namespace Dominio.Entidad.Respuestas
{
  public class RespuestaConsultarReferenciaDocumento
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Message { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public string? Plataforma { get; set; }
    public IEnumerable<Extras>? Extras { get; set; }
    public Rango? Rango { get; set; }
    public Sucursales? Sucursales { get; set; }
    public List<ListaRetenciones>? Retenciones { get; set; }
    public FacturaGeneral? Factura { get; set; }
    public string? DocumentosAdjuntos { get; set; }
    public string? IdUsuario { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
