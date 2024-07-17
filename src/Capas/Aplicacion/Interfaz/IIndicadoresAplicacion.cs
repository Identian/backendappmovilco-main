using Aplicacion.Dto.Solicitudes.FiltrosSolicitudes;
using Newtonsoft.Json.Linq;

namespace Aplicacion.Interfaz
{
  public interface IIndicadoresAplicacion
  {
    public JToken ConsultarTotalDocumentos(FiltrosTotalDocumentosDto filtrosDto, string bearerToken, string valorBearerToken);
  }
}
