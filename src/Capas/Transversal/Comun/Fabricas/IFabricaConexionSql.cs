using System.Data;

namespace Transversal.Comun.Fabricas
{
  public interface IFabricaConexionSql
  {
    IDbConnection ConexionSoloLectura { get; }
    IDbConnection ConexionLecturaEscritura { get; }
    IDbConnection ConexionConfiguracion { get; }
  }
}
