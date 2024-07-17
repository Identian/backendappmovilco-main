namespace Dominio.Entidad.Documentos
{
  public class FacturaGeneral
  {
    public string? IdRangoNumeracion { get; set; }
    public string? TipoDocumento { get; set; }
    public string? RangoNumeracion { get; set; }
    public string? ConsecutivoDocumento { get; set; }
    public string? FechaEmision { get; set; }
    public string? Moneda { get; set; }
    public string? Propina { get; set; }
    public string? IdInvoicePorCorregir { get; set; }
    public string? CantidadDecimales { get; set; }
    public string? TotalDescuentos { get; set; }
    public string? TotalSinImpuestos { get; set; }
    public string? FechaVencimiento { get; set; }
    public IEnumerable<string>? InformacionAdicional { get; set; }
    public Cliente? Cliente { get; set; }
    public IEnumerable<FacturaImpuestos>? ImpuestosGenerales { get; set; }
    public IEnumerable<FacturaDetalle>? DetalleDeFactura { get; set; }
    public IEnumerable<Extras>? Extras { get; set; }
    public string? RedondeoAplicado { get; set; }
    public string? TotalAnticipos { get; set; }
    public string? TotalCargosAplicados { get; set; }
    public string? TotalBrutoConImpuesto { get; set; }
    public string? TotalBaseImponible { get; set; }
    public string? TotalMonto { get; set; }
    public IEnumerable<ImpuestosTotales>? ImpuestosTotales { get; set; }
    public IEnumerable<CargosDescuentos>? CargosDescuentos { get; set; }
    public string? FechaInicioPeriodoFacturacion { get; set; }
    public string? FechaFinPeriodoFacturacion { get; set; }
    public Entrega? EntregaMercancia { get; set; }
    public TerminosDeEntrega? TerminosEntrega { get; set; }
    public IEnumerable<MediosDePago>? MediosDePago { get; set; }
    public IEnumerable<Anticipos>? Anticipos { get; set; }
    public TasaDeCambioBase? TasaDeCambio { get; set; }
    public TasaDeCambioAlternativa? TasaDeCambioAlternativa { get; set; }
    public string? TotalProductos { get; set; }
    public Autorizado? Autorizado { get; set; }
    public IEnumerable<DocumentoReferenciado>? DocumentosReferenciados { get; set; }
    public IEnumerable<OrdenDeCompra>? OrdenDeCompra { get; set; }
    public string? FechaPagoImpuestos { get; set; }
    public string? TipoOperacion { get; set; }
    public string? Namefile { get; set; }
    public IEnumerable<CondicionDePago>? CondicionPago { get; set; }
    public IEnumerable<FacturaImpuestos>? RetencionesGenerales { get; set; }
    public string? TipoSector { get; set; }
    public SectorSalud? SectorSalud { get; set; }
  }
}
