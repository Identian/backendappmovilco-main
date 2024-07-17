using Dominio.Entidad.Empresas;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Infraestructura.Interfaz
{
  public interface ISecuencialesRepositorio
  {
    public RespuestaConsultarNumeraciones ConsultarNumeraciones(SolicitudConsultarFacturacion solicitudConsultarNumeraciones);
    public RespuestaSeleccionarSecuencial Seleccionar(SolicitudSeleccionarSecuencial solicitud, EmpresaAutenticacion datosToken);
  }
}
