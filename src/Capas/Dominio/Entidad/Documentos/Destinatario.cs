namespace Dominio.Entidad.Documentos
{
  public class Destinatario
  {
    public string? CanalDeEntrega { get; set; }
    public IEnumerable<string?>? Email { get; set; }
    public string? NitProveedorReceptor { get; set; }
    public string? Telefono { get; set; }
    public string? MensajePersonalizado { get; set; }
    public string? FechaProgramada { get; set; }
    public IEnumerable<Extensible>? Extras { get; set; }
  }
}
