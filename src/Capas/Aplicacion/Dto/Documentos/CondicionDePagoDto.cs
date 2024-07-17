namespace Aplicacion.Entidad.Documentos
{
  public class CondicionDePagoDto
  {
    public string? Identificador { get; set; }
    public string? MedioPagoAsociado { get; set; }
    public string? Comentario { get; set; }
    public string? MontoMulta { get; set; }
    public string? Monto { get; set; }
    public string? PorcentajePago { get; set; }
    public string? MontoPenalidad { get; set; }
    public string? CodigoEvento { get; set; }
    public string? DuracionPeriodo { get; set; }
    public string? DuracionPeriodoMedida { get; set; }
    public string? FechaVencimiento { get; set; }
    public string? ReferenciaAnticipo { get; set; }
    public string? PeriodoDesde { get; set; }
    public string? PeriodoHasta { get; set; }
    public string? PorcentajeDescuento { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
