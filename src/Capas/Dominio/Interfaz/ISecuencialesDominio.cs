using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Dominio.Interfaz
{
  public interface ISecuencialesDominio
  {
    public RespuestaConsultarNumeraciones ConsultarNumeraciones(SolicitudConsultarFacturacion solicitudConsultarNumeraciones);
    public RespuestaSeleccionarSecuencial Seleccionar(SolicitudSeleccionarSecuencial solicitud, string bearerToken, string valorBearerToken);
  }
}
