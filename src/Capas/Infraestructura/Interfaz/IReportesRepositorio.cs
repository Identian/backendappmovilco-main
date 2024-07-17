using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Newtonsoft.Json.Linq;

namespace Infraestructura.Interfaz
{
  public interface IReportesRepositorio
  {
    public RespuestaReporteEnLinea ReporteEnLinea(SolicitudReporteEnLinea solicitudReporteEnLinea, string bearerToken);
    public string ObtenerCodigoReporteGeneralEnLinea(string sistema);
 
  }
}
