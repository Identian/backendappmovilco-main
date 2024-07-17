using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Newtonsoft.Json.Linq;

namespace Infraestructura.Interfaz
{
  public interface IEmpresaRepositorio
  {
    public JToken ConsultarEmpresa(SolicitudConsultarFacturacion solicitud);
    public RespuestaConsultarEmpresaFacturacion ConsultarEmpresaPorId(int idEmpresa);
  }
}
