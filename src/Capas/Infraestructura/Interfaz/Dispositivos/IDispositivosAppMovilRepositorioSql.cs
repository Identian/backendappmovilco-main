using Dominio.Entidad.Dispositivos;
using Dominio.Entidad.Respuestas;
using Newtonsoft.Json.Linq;
using Transversal.Comun.Respuestas;

namespace Infraestructura.Interfaz
{
  public interface IDispositivosAppMovilRepositorioSql
  {
    public JToken? ConsultarNombreYSerialDispositivo(Dispositivo solicitud, string Token);
    public RespuestaValidadSubscripcion ValidarSuscripcionDispositivoPorSerialLogico(string idEmpresa, string serialLogico);
    public JToken? ConsultarSuscripcionDispositivoPorSerialLogico(string idEmpresa, string serialLogico);
  }
}
