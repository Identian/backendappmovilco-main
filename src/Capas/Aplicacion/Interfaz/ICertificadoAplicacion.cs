using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;

namespace Aplicacion.Interfaz
{
  public interface ICertificadoAplicacion
  {
    public RespuestaConsultarCertificadoFacturacionDto Consultar(SolicitudConsultarFacturacionDto solicitudDto);
  }
}
