namespace Aplicacion.Entidad.Documentos
{
  public class DestinatarioDto
  {
    public string? CanalDeEntrega { get; set; }
    public IEnumerable<string?>? Email { get; set; }
    public string? NitProveedorReceptor { get; set; }
    public string? Telefono { get; set; }
    public string? MensajePersonalizado { get; set; }
    public string? FechaProgramada { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
