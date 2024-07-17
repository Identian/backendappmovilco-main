namespace Aplicacion.Entidad.Documentos
{
  public class ImpuestosTotalesDto
  {
    public string? CodigoTOTALImp { get; set; }
    public string? MontoTotal { get; set; }
    public string? RedondeoAplicado { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
