using Dapper;
using Dominio.Entidad;
using Dominio.Entidad.Respuestas;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using System.Data;
using Transversal.Comun.Fabricas;

namespace Infraestructura.Repositorio
{
  public class UsuarioRepositorio : IUsuarioRepositorio
  {
    private readonly IFabricaConexionSql _fabricaConexionSql;
    private readonly IConfiguration _configuracion;

    public UsuarioRepositorio(IFabricaConexionSql fabricaConexionSql, IConfiguration configuracion)
    {
      _fabricaConexionSql = fabricaConexionSql;
      _configuracion = configuracion;
    }

    public RespuestaConsultarUsuarioFacturacion ConsultarInformacion(string idEmpresa, string nitEmpresa, string correoUsuario)
    {
      RespuestaConsultarUsuarioFacturacion respuesta = new();
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarUsuarioDeEmpresaPorCorreo:ProcedimientoAlmacenado"]!;
      int tiempoDeEspera = Convert.ToInt32(_configuracion["BaseDeDatos:EjecucionProcedimientoAlmacenado:TiempoDeEspera"]);
      var parametros = new DynamicParameters();
      parametros.Add("IdEmpresa", idEmpresa);
      parametros.Add("CorreoUsuario", correoUsuario);
      var datosUsuario = conexion.QuerySingleOrDefault<UsuarioFacturacion>(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: tiempoDeEspera);
      if (datosUsuario != null)
      {

        respuesta.Codigo = 200;
        respuesta.Resultado = "Exitoso";
        respuesta.Mensaje = "Consulta Exitosa";
        respuesta.Nit = nitEmpresa;
        respuesta.IdEmpresa = idEmpresa;
        respuesta.Usuario = datosUsuario;
      }
      else
      {
        respuesta.Codigo = 404;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "No se encontraron datos para el usuario: " + correoUsuario;
      }
      return respuesta;
    }

    public RespuestaConsultarRollUsuario ConsultarRollUsuario(string idUsuario)
    {
      RespuestaConsultarRollUsuario respuesta = new();
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarRollUsuario:ProcedimientoAlmacenado"]!;
      int tiempoDeEspera = Convert.ToInt32(_configuracion["BaseDeDatos:EjecucionProcedimientoAlmacenado:TiempoDeEspera"]);
      var parametros = new DynamicParameters();
      parametros.Add("IdUsuario", idUsuario);
      var datosRollUsuario = conexion.Query<RollUsuarioFacturacion>(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: tiempoDeEspera);
      if (datosRollUsuario != null)
      {

        respuesta.Codigo = 200;
        respuesta.Resultado = "Exitoso";
        respuesta.Mensaje = "Consulta Exitosa";
        respuesta.IdUsuario = idUsuario;
        respuesta.rollUsuario = (List<RollUsuarioFacturacion>?)datosRollUsuario;
      }
      else
      {
        respuesta.Codigo = 404;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "No se encontraron datos para el usuario: " + idUsuario;
        respuesta.rollUsuario = null;
      }
      return respuesta;
    }
  }
}
