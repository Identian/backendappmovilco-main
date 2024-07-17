using Newtonsoft.Json.Linq;

namespace Infraestructura.Interfaz
{
  public interface ICatalogosRepositorio
  {
    public JToken? Consultar(string identificador);
  }
}
