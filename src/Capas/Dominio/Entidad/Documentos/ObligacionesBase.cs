namespace Dominio.Entidad.Documentos
{
  public class ObligacionesBase
  {
    public string? Obligaciones { get; set; }
    public string? Regimen { get; set; }
    public IEnumerable<Extensible>? Extras { get; set; }
  }
}
