using Aplicacion.Dto.Dispositivos;
using Aplicacion.Dto.Solicitudes.Dispositivos;
using Newtonsoft.Json.Linq;

namespace Aplicacion.Interfaz
{
  public interface IDispositivosAplicacion
  {
    public JToken? CrearAccesoDispositivo(DispositivoDto solicitudDto, string bearerToken, string valorBearerToken);
    public JToken? ConsultarSuscripcionDispositivo(SolicitudConsultarSuscripcionDispositivoDto solicitudDto, string bearerToken, string valorBearerToken);
    public JToken? AsociarAlias(SolicitudAsociarAliasDto solicitudDto, string bearerToken, string valorBearerTokenn);
  }
}
