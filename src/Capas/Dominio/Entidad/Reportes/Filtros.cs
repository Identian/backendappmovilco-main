namespace Dominio.Entidad.Reportes
{
  public class Filtros
  {
    public string? CodigoReporte { get; set; }
    public string? FechaInicio { get; set; }
    public string? FechaHasta { get; set; }
    public string? TipoIdentificacionAdquiriente { get; set; }
    public string? NumeroIdentificacionAdquiriente { get; set; }
    public IEnumerable<string>? EstatusDian { get; set; }
    public IEnumerable<string>? EstatusAcuseRecibo { get; set; }
    public IEnumerable<string>? EstatusCorreoElectronico { get; set; }
    public IEnumerable<string>? EstatusHka { get; set; }
    public IEnumerable<string>? TipoDocumento { get; set; }
    public string? RangoMontoInicial { get; set; }
    public string? RangoMontoFinal { get; set; }
    public IEnumerable<string>? OrigenFacturacion { get; set; }
    public string? Prefijo { get; set; }
    public string? NumeracionFacturaInical { get; set; }
    public string? NumeracionFacturaInicial { get; set; }
    public string? NumeracionFacturaFinal { get; set; }
    public IEnumerable<string>? CodigoSucursal { get; set; }
    public string? Ambiente { get; set; }
  }
}
