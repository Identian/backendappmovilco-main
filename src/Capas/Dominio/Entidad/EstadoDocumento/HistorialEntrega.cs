namespace Dominio.Entidad.EstadoDocumento
{
  public class HistorialEntrega
  {
    public string? CanalDeEntrega { get; set; }
    public List<string>? Email { get; set; }
    public string? NitProveedorReceptor { get; set; }
    public string? Telefono { get; set; }
    public string? MensajePersonalizado { get; set; }
    public string? FechaProgramada { get; set; }
    public string? EntregaEstatus { get; set; }
    public string? EntregaEstatusDescripcion { get; set; }
    public string? EntregaFecha { get; set; }
    public string? RecepcionEmailComentario { get; set; }
    public string? RecepcionEmailEstatus { get; set; }
    public string? RecepcionEmailFecha { get; set; }
    public string? RecepcionEmailIPAddress { get; set; }
    public string? LeidoEstatus { get; set; }
    public string? LeidoFecha { get; set; }
    public string? LeidoEmailIPAddress { get; set; }
  }
}
