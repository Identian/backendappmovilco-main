using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Dominio.Interfaz
{
  public interface ICertificadoDominio
  {
    public RespuestaConsultarCertificadoFacturacion Consultar(SolicitudConsultarFacturacion solicitud);
  }
}
