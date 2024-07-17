using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Dominio.Interfaz
{
  public interface IFoliosDominio
  {
    public RespuestaConsultarResumenFolios ConsultarResumen(string bearerToken);
  }
}
