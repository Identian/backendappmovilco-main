using Dominio.Entidad.Reportes;

namespace Dominio.Entidad.Solicitudes
{
  public class SolicitudSeleccionarEstablecimiento
  {
    public string? IdEstablecimiento { get; set; }
    public string? Seleccionado { get; set; }
    public string? Referencia { get; set; }
  }
}
