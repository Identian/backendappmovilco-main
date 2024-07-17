using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;

namespace Aplicacion.Interfaz
{
  public interface IEstablecimientosAplicacion
  {
    public RespuestaSeleccionarEstablecimientoDto Seleccionar(SolicitudSeleccionarEstablecimientoDto solicitudDto, string bearerToken, string valorBearerToken);
  }
}
