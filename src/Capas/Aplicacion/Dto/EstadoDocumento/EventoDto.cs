namespace Aplicacion.Dto.EstadoDocumento
{
  public class EventoDto
  {
    public string? tipoEvento { get; set; }
    public string? versionUBL { get; set; }
    public string? idPerfilDIAN { get; set; }
    public string? ambienteDIAN { get; set; }
    public string? descripcionEvento { get; set; }
    public string? numeroDelEvento { get; set; }
    public string? emisorRazonSocial { get; set; }
    public string? emisorTipoIdentificacion { get; set; }
    public string? emisorNumeroDocumento { get; set; }
    public string? emisorNumeroDocumentoDV { get; set; }
    public string? receptorRazonSocial { get; set; }
    public string? receptorTipoIdentificacion { get; set; }
    public string? receptorNumeroDocumento { get; set; }
    public string? receptorNumeroDocumentoDV { get; set; }
    public string? nombreArchivoXML { get; set; }
    public string? fechaEmision { get; set; }
    public string? fechaRecepcion { get; set; }
    public string? nota { get; set; }
    public string? codigo { get; set; }
    public string? mensaje { get; set; }
    public string? comentario { get; set; }
    public string? tipoCufe { get; set; }
    public string? cufe { get; set; }
    public string? hash { get; set; }
    public string? resultado { get; set; }
    public List<ExtensibleDto>? extras { get; set; }
  }
}
