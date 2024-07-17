using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Dominio.Interfaz
{
  public interface ICatalogosDominio
  {
    public RespuestaConsultarCatalogo Consultar(SolicitudConsultarCatalogo solicitud);
  }
}
