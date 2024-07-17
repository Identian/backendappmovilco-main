using Dapper;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using Transversal.Comun.Fabricas;

namespace Infraestructura.Repositorio
{
  public class CatalogosRepositorio : ICatalogosRepositorio
  {
    private readonly IFabricaConexionSql _fabricaConexionSql;
    private readonly IConfiguration _configuracion;

    public CatalogosRepositorio(IFabricaConexionSql fabricaConexionSql, IConfiguration configuracion)
    {
      _fabricaConexionSql = fabricaConexionSql;
      _configuracion = configuracion;
    }

    public JToken? Consultar(string identificador)
    {
      JToken? respuesta = null;
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      var consultar = _configuracion[string.Concat("BaseDeDatos:SoloLectura:Catalogos:", identificador)];
      var catalogo = conexion.Query(sql: consultar, commandType: CommandType.StoredProcedure);
      if (catalogo != null)
      {
        string cadenaJson = string.Concat("{", "\"", identificador, "\":", JsonConvert.SerializeObject(catalogo.Select(r => (object)r)), "}");
        var objetoJson = JObject.Parse(cadenaJson);
        respuesta = objetoJson;
      }
      return respuesta;
    }
  }
}
