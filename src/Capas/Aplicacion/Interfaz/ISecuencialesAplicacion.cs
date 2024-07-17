using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;

namespace Aplicacion.Interfaz
{
  public interface ISecuencialesAplicacion
  {
    public RespuestaConsultarNumeracionesDto ConsultarNumeraciones(SolicitudConsultarFacturacionDto solicitudConsultarNumeracionesDto);
    public RespuestaSeleccionarSecuencialDto Seleccionar(SolicitudSeleccionarSecuencialDto solicitudDto, string bearerToken, string valorBearerToken);
  }
}
