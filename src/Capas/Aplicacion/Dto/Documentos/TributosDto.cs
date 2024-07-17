namespace Aplicacion.Entidad.Documentos
{
  public class TributosDto
  {
    public string? CodigoImpuesto { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
