using Dominio.Entidad.Solicitudes;
using Newtonsoft.Json.Linq;

namespace Infraestructura.Interfaz
{
  public interface IIndicadoresRepositorio
  {
    public JToken ConsultarTotalDocumentos(SolicitudConsultarTotalDocumentos solicitud);
  }
}
