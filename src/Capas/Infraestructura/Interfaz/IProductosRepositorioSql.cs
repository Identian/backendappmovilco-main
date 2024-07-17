using Dominio.Entidad.Documentos;
using Dominio.Entidad.Respuestas;

namespace Infraestructura.Interfaz
{
  public interface IProductosRepositorioSql
  {
    public RespuestaConsultarProducto ConsultarPorId(int idProducto, int idEmpresa);
  }
}
