using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Infraestructura.Interfaz
{
  public interface IDeliveryRepositorio
  {
    public RespuestaEnviarCorreoIndividual EnviarCorreoIndividual(SolicitudEnviarCorreoIndividual solicitud, string bearerToken, string valorBearerToken);
  }
}
