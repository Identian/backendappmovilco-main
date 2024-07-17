using Dominio.Entidad.Reportes;

namespace Dominio.Entidad.Solicitudes
{
  public class SolicitudAgendarReporteProgramadoFacturacion : SolicitudConClaveSecretaFacturacion
  {
    public AgendarReporteProgramado? Reporte { get; set; }
  }
}
