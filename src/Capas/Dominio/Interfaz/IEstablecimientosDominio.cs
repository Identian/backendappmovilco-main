using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Dominio.Interfaz
{
  public interface IEstablecimientosDominio
  {
    public RespuestaSeleccionarEstablecimiento Seleccionar(SolicitudSeleccionarEstablecimiento solicitud ,string bearerToken, string valorBearerToken);
  }
}
