using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Infraestructura.Interfaz
{
  public interface ICertificadoRepositorio
  {
    public RespuestaConsultarCertificadoFacturacion Consultar(SolicitudConsultarFacturacion solicitud);
  }
}
