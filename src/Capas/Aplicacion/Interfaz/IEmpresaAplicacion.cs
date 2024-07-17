using Aplicacion.Dto.Solicitudes;
using Newtonsoft.Json.Linq;

namespace Aplicacion.Interfaz
{
  public interface IEmpresaAplicacion
  {
    public JToken ConsultarEmpresa(SolicitudConsultarFacturacionDto solicitudDto, string bearerToken, string valorBearerToken);
  }
}
