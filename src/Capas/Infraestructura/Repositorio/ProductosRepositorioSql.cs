using Dapper;
using Dominio.Entidad.Documentos;
using Dominio.Entidad.Respuestas;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using System.Data;
using Transversal.Comun.Fabricas;

namespace Infraestructura.Repositorio
{
  public class ProductosRepositorioSql : IProductosRepositorioSql
  {
    private readonly IFabricaConexionSql _fabricaConexionSql;
    private readonly IConfiguration _configuracion;

    public ProductosRepositorioSql(IFabricaConexionSql fabricaConexionSql, IConfiguration configuracion)
    {
      _fabricaConexionSql = fabricaConexionSql;
      _configuracion = configuracion;
    }

    public RespuestaConsultarProducto ConsultarPorId(int idProducto, int idEmpresa)
    {
      RespuestaConsultarProducto respuesta = new();
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string? consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarProductoDeEmpresaPorId:ProcedimientoAlmacenado"];
      DynamicParameters parametros = new();
      parametros.Add("IdProducto", idProducto);
      parametros.Add("IdEmpresa", idEmpresa);
      var producto = conexion.QuerySingleOrDefault<FacturaDetalle>(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);
      if (producto != null)
      {
        respuesta.Codigo = 200;
        respuesta.Resultado = "Exitoso";
        respuesta.Mensaje = "Consulta Exitosa";
        respuesta.Datos = producto;
      }
      else
      {
        respuesta.Codigo = 404;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "No se encontró el producto dentro de los registrados a esta empresa";
      }
      return respuesta;
    }
  }
}
