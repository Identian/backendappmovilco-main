using Aplicacion.Entidad.Documentos;
using Aplicacion.Entidad.ReferenciaDocumento;

namespace Aplicacion.Entidad.Respuestas
{
  public class RespuestaConsultarReferenciaDocumentoDto
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public string? Plataforma { get; set; }
    public IEnumerable<ExtrasDto>? Extras { get; set; }
    public RangoDto? Rango { get; set; }
    public SucursalesDto? Sucursales { get; set; }
    public List<ListaRetencionesDto>? Retenciones { get; set; }
    public FacturaGeneralDto? Factura { get; set; }
    public string? DocumentosAdjuntos { get; set; }
    public string? IdUsuario { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
