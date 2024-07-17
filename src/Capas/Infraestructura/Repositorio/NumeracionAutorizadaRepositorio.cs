using Dapper;
using Dominio.Entidad;
using Dominio.Entidad.Documentos;
using Dominio.Entidad.Respuestas;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using System.Data;
using Transversal.Comun.Fabricas;

namespace Infraestructura.Repositorio
{
  public class NumeracionAutorizadaRepositorio : INumeracionAutorizadaRepositorio
  {
    private readonly IFabricaConexionSql _fabricaConexionSql;
    private readonly IConfiguration _configuracion;

    public NumeracionAutorizadaRepositorio(IFabricaConexionSql fabricaConexionSql, IConfiguration configuracion)
    {
      _fabricaConexionSql = fabricaConexionSql;
      _configuracion = configuracion;
    }
    public RespuestaConsultarNumeracionAutorizada ConsultarNumeracionAutorizada(string idNumeracion, string idEmpresa)
    {
      RespuestaConsultarNumeracionAutorizada respuesta = new();
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string? consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarNumeracionAutorizadaPorId:ProcedimientoAlmacenado"];
      DynamicParameters parametros = new();
      parametros.Add("IdNumeracion", idNumeracion);
      parametros.Add("IdEmpresa", idEmpresa);
      var numeracion = conexion.QuerySingleOrDefault<NumeracionAutorizada>(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);
      if (numeracion != null)
      {
        respuesta.Codigo = 200;
        respuesta.Resultado = "Exitoso";
        respuesta.Mensaje = "Consulta Exitosa";      
        respuesta.Datos = numeracion;
      }
      else
      {
        respuesta.Codigo = 404;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "No se encontró la numeracion autorizada para este cliente";
      }
      return (respuesta);
    }


  }
}
