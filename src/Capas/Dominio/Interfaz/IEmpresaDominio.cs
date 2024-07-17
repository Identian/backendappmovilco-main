using Dominio.Entidad.Solicitudes;
using Newtonsoft.Json.Linq;

namespace Dominio.Interfaz
{
  public interface IEmpresaDominio
  {
    public JToken ConsultarEmpresa(SolicitudConsultarFacturacion solicitud, string bearerToken, string valorBearerToken);
  }
}
