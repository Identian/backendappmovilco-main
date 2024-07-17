namespace Aplicacion.Entidad.Documentos
{
  public class ObligacionesBaseDto
  {
    public string? Obligaciones { get; set; }
    public string? Regimen { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
