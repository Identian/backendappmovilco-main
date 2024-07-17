using Dominio.Entidad.Dispositivos;
using Dominio.Entidad.Solicitudes;
using Newtonsoft.Json.Linq;

namespace Dominio.Interfaz
{
  public interface IDispositivosDominio
  {
    public JToken? CrearAccesoDispositivo(Dispositivo solicitud, string bearerToken, string valorBearerToken);
    public JToken? ConsultarSuscripcionDispositivo(string serialLogico, string bearerToken, string valorBearerToken);
    public JToken? AsociarAlias(SolicitudAsociarAlias solicitud, string bearerToken, string valorBearerToken);
  }
}
