namespace Aplicacion.Entidad.Documentos
{
  public class AutorizadoDto
  {
    public string? NombreComercial { get; set; }
    public string? RazonSocial { get; set; }
    public string? NumeroDocumento { get; set; }
    public string? NumeroDocumentoDV { get; set; }
    public string? TipoIdentificacion { get; set; }
    public DireccionBaseReferenciaDto? Direccion { get; set; }
    public string? NombreContacto { get; set; }
    public string? Telefax { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Nota { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
