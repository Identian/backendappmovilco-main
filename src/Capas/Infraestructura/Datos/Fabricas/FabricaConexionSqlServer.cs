using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Transversal.Comun.Fabricas;

namespace Infraestructura.Datos.Fabricas
{
  public class FabricaConexionSqlServer : IFabricaConexionSql
  {
    private readonly IConfiguration _configuracion;

    public FabricaConexionSqlServer(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }

    private IDbConnection Conexion(string tipo)
    {
      var conexionSql = new SqlConnection()
      {
        ConnectionString = _configuracion[string.Concat("BaseDeDatos:", tipo, ":CadenaConexion")]
      };
      conexionSql.Open();
      return (conexionSql);
    }

    public IDbConnection ConexionSoloLectura
    {
      get
      {
        return (Conexion("SoloLectura"));
      }
    }

    public IDbConnection ConexionLecturaEscritura
    {
      get
      {
        return (Conexion("LecturaEscritura"));
      }
    }

    public IDbConnection ConexionConfiguracion
    {
      get
      {
        return (Conexion("Configuracion"));
      }
    }
  }
}