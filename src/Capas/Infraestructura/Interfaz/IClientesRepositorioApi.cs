using Dominio.Entidad.Documentos;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Infraestructura.Interfaz
{
  public interface IClientesRepositorioApi
  {
    public Cliente ObtenerInformacionCliente(int idCliente);
    public Cliente ObtenerInformacionClienteEscenarioNull();

  }
}
