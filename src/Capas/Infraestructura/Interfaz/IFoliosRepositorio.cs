using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Infraestructura.Interfaz
{
  public interface IFoliosRepositorio
  {
    public RespuestaConsultarResumenFolios ConsultarResumen(string idEmpresa, string token);
  }
}
