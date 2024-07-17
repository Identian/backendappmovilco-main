namespace Aplicacion.Entidad.Documentos
{
  public class DocumentoReferenciadoDto
  {
    public string? NumeroDocumento { get; set; }
    public string? CodigoNumeroDocumento { get; set; }
    public string? NombreNumeroDocumento { get; set; }
    public string? CufeDocReferenciado { get; set; }
    public string? TipoCUFE { get; set; }
    public string? Fecha { get; set; }
    public string? TipoDocumento { get; set; }
    public string? TipoDocumentoCodigo { get; set; }
    public string? TipoDocumentoCodigo_url { get; set; }
    public string? CodigoInterno { get; set; }
    public string? CodigoEstatusDocumento { get; set; }
    public IEnumerable<string>? Descripcion { get; set; }
    public string? FechaInicioValidez { get; set; }
    public string? FechaFinValidez { get; set; }
    public string? Monto { get; set; }
    public string? ConceptoRecaudo { get; set; }
    public string? NumeroIdentificacion { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
