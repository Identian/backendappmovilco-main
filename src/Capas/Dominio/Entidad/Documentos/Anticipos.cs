namespace Dominio.Entidad.Documentos
{
  public class Anticipos
  {
    public string? Id { get; set; }
    public string? MontoPagado { get; set; }
    public string? FechaDeRecibido { get; set; }
    public string? FechaDePago { get; set; }
    public string? HoraDePago { get; set; }
    public string? Instrucciones { get; set; }
    public IEnumerable<Extensible>? Extras { get; set; }
  }
}
