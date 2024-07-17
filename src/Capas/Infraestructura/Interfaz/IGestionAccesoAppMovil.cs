using Dominio.Entidad.Dispositivos;
using Dominio.Entidad.Solicitudes;
using Newtonsoft.Json.Linq;

namespace Infraestructura.Interfaz
{
  public interface IGestionAccesoAppMovil
  {
    public dynamic? GestionAccesoEmpresa(string Token, string idEmpresa);
    public dynamic? GestionAccesoUsuario(string Token, string idEmpresa, string idUsuario);
    public JToken? CrearAccesoDispositivo(Dispositivo solicitud, string Token);
    public JToken? ConsultarSuscripcionDispositivo(string idEmpresa, string idSuscripcion, string token);
    public JToken? AsociarAlias(SolicitudAsociarAlias solicitud, string token);
  }
}
