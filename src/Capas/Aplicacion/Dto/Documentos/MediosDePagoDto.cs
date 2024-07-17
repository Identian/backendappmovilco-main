namespace Aplicacion.Entidad.Documentos
{
  public class MediosDePagoDto
  {
    public string? MetodoDePago { get; set; }
    public string? MedioPago { get; set; }
    public string? FechaDeVencimiento { get; set; }
    public string? NumeroDeReferencia { get; set; }
    public string? CodigoReferencia { get; set; }
    public string? NumeroDias { get; set; }
    public string? CodigoBanco { get; set; }
    public string? NombreBanco { get; set; }
    public string? NumeroTransferencia { get; set; }
    public string? CodigoCanalPago { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
