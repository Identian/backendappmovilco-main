using Dominio.Entidad.Solicitudes.FiltrosSolicitudes;
using Newtonsoft.Json.Linq;

namespace Dominio.Interfaz
{
  public interface IIndicadoresDominio
  {
    public JToken ConsultarTotalDocumentos(FiltrosTotalDocumentos filtros, string bearerToken, string valorBearerToken);
  }
}
