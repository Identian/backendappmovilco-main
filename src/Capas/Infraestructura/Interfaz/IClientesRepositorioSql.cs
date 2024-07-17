using Dominio.Entidad.Respuestas;

namespace Infraestructura.Interfaz
{
  public interface IClientesRepositorioSql
  {
    public RespuestaConsultarCliente ConsultarPorId(string idCliente, string idEmpresa);
  }
}
