namespace Dominio.Entidad.Documentos
{
  public class OrdenDeCompra
  {
    public string? NumeroOrden { get; set; }
    public string? Fecha { get; set; }
    public string? NumeroPedido { get; set; }
    public string? TipoOrden { get; set; }
    public string? CodigoCliente { get; set; }
    public string? Uuid { get; set; }
    public string? TipoCUFE { get; set; }
    public DocumentoReferenciado? DocumentoReferencia { get; set; }
    public IEnumerable<Extensible>? Extras { get; set; }
  }
}
