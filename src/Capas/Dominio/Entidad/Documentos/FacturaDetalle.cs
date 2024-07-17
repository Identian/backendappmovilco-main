using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Documentos
{
  public class FacturaDetalle
  {
    public string? IdProducto { get; set; }
    public string? Secuencia { get; set; }
    public string? CodigoProducto { get; set; }
    public string? Descripcion { get; set; }
    public string? Descripcion2 { get; set; }
    public string? Descripcion3 { get; set; }
    public string? CantidadUnidades { get; set; }
    public string? UnidadMedida { get; set; }
    public string? PrecioVentaUnitario { get; set; }
    public string? PrecioTotalSinImpuestos { get; set; }
    public string? PrecioTotal { get; set; }
    public string? Seriales { get; set; }
    public string? MuestraGratis { get; set; }
    public string? Nota { get; set; }
    public string? PrecioReferencia { get; set; }
    public string? CodigoTipoPrecio { get; set; }
    public string? CantidadPorEmpaque { get; set; }
    public string? DescripcionTecnica { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? SubCodigoProducto { get; set; }
    public string? CodigoFabricante { get; set; }
    public string? SubCodigoFabricante { get; set; }
    public string? NombreFabricante { get; set; }
    public string? EstandarCodigoProducto { get; set; }
    public string? EstandarCodigo { get; set; }
    public string? EstandarCodigoID { get; set; }
    public string? EstandarCodigoNombre { get; set; }
    public string? EstandarCodigoIdentificador { get; set; }
    public string? EstandarSubCodigoProducto { get; set; }
    public string? EstandarOrganizacion { get; set; }
    public string? CodigoIdentificadorPais { get; set; }
    public string? MandatorioNumeroIdentificacion { get; set; }
    public string? MandatorioNumeroIdentificacionDV { get; set; }
    public string? MandatorioTipoIdentificacion { get; set; }
    public IEnumerable<FacturaImpuestos>? ImpuestosDetalles { get; set; }
    public IEnumerable<ImpuestosTotales>? ImpuestosTotales { get; set; }
    public IEnumerable<CargosDescuentos>? CargosDescuentos { get; set; }
    public IEnumerable<LineaInformacionAdicional>? InformacionAdicional { get; set; }
    public IEnumerable<DocumentoReferenciado>? DocumentosReferenciados { get; set; }
    public string? CantidadReal { get; set; }
    public string? CantidadRealUnidadMedida { get; set; }
    public IEnumerable<Extensible>? Extras { get; set; }
    public string? TipoAIU { get; set; }
    public string? IdEsquema { get; set; }
  }
}
