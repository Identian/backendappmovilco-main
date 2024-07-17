using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;

namespace Aplicacion.Interfaz
{
  public interface ICatalogosAplicacion
  {
    public RespuestaConsultarCatalogoDto Consultar(SolicitudConsultarCatalogoDto solicitudDto);
  }
}
