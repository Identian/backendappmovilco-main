namespace Aplicacion.Entidad.Documentos
{
  public class FacturaImpuestosDto
  {
    public string? CodigoTOTALImp { get; set; }
    public string? PorcentajeTOTALImp { get; set; }
    public string? BaseImponibleTOTALImp { get; set; }
    public string? ValorTOTALImp { get; set; }
    public string? ControlInterno { get; set; }
    public string? UnidadMedidaTributo { get; set; }
    public string? UnidadMedida { get; set; }
    public string? ValorTributoUnidad { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
