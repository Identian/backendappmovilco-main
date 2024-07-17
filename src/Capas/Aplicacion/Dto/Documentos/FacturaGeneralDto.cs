namespace Aplicacion.Entidad.Documentos
{
  public class FacturaGeneralDto
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
    public ClienteDto? Cliente { get; set; }
    public IEnumerable<FacturaImpuestosDto>? ImpuestosGenerales { get; set; }
    public IEnumerable<FacturaDetalleDto>? DetalleDeFactura { get; set; }
    public IEnumerable<ExtrasDto>? Extras { get; set; }
    public string? RedondeoAplicado { get; set; }
    public string? TotalAnticipos { get; set; }
    public string? TotalCargosAplicados { get; set; }
    public string? TotalBrutoConImpuesto { get; set; }
    public string? TotalBaseImponible { get; set; }
    public string? TotalMonto { get; set; }
    public IEnumerable<ImpuestosTotalesDto>? ImpuestosTotales { get; set; }
    public IEnumerable<CargosDescuentosDto>? CargosDescuentos { get; set; }
    public string? FechaInicioPeriodoFacturacion { get; set; }
    public string? FechaFinPeriodoFacturacion { get; set; }
    public EntregaDto? EntregaMercancia { get; set; }
    public TerminosDeEntregaDto? TerminosEntrega { get; set; }
    public IEnumerable<MediosDePagoDto>? MediosDePago { get; set; }
    public IEnumerable<AnticiposDto>? Anticipos { get; set; }
    public TasaDeCambioBaseDto? TasaDeCambio { get; set; }
    public TasaDeCambioAlternativaDto? TasaDeCambioAlternativa { get; set; }
    public string? TotalProductos { get; set; }
    public AutorizadoDto? Autorizado { get; set; }
    public IEnumerable<DocumentoReferenciadoDto>? DocumentosReferenciados { get; set; }
    public IEnumerable<OrdenDeCompraDto>? OrdenDeCompra { get; set; }
    public string? FechaPagoImpuestos { get; set; }
    public string? TipoOperacion { get; set; }
    public string? Namefile { get; set; }
    public IEnumerable<CondicionDePagoDto>? CondicionPago { get; set; }
    public IEnumerable<FacturaImpuestosDto>? RetencionesGenerales { get; set; }
    public string? TipoSector { get; set; }
    public SectorSaludDto? SectorSalud { get; set; }
  }
}
