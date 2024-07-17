namespace Dominio.Entidad.Documentos
{
  public class Tributos
  {
    public string? CodigoImpuesto { get; set; }
    public IEnumerable<Extensible>? Extras { get; set; }
  }
}
