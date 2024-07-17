namespace Dominio.Entidad.Documentos
{
  public class TasaDeCambioBase
  {
    public string? MonedaOrigen { get; set; }
    public string? BaseMonedaOrigen { get; set; }
    public string? MonedaDestino { get; set; }
    public string? BaseMonedaDestino { get; set; }
    public string? TasaDeCambio { get; set; }
    public string? FechaDeTasaDeCambio { get; set; }
    public string? IndicadorDeTasa { get; set; }
    public string? OperadorCalculo { get; set; }
    public IEnumerable<Extensible>? Extras { get; set; }
  }
}
