namespace Aplicacion.Entidad.Documentos
{
  public class OrdenDeCompraDto
  {
    public string? NumeroOrden { get; set; }
    public string? Fecha { get; set; }
    public string? NumeroPedido { get; set; }
    public string? TipoOrden { get; set; }
    public string? CodigoCliente { get; set; }
    public string? Uuid { get; set; }
    public string? TipoCUFE { get; set; }
    public DocumentoReferenciadoDto? DocumentoReferencia { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
