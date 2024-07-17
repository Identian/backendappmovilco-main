using AutoMapper;
using Dapper;
using Dominio.Entidad;
using Dominio.Entidad.Empresas;
using Dominio.Entidad.Respuestas;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using Transversal.Comun.Fabricas;

namespace Infraestructura.Repositorio
{
  public class EmpresaAutenticacionRepositorio : IEmpresaAutenticacionRepositorio
  {

    private readonly IMapper _mapeador;
    private readonly IFabricaConexionSql _fabricaConexionSql;
    private readonly IConfiguration _configuracion;

    public EmpresaAutenticacionRepositorio(IMapper mapeador, IFabricaConexionSql fabricaConexionSql, IConfiguration configuracion)
    {
      _fabricaConexionSql = fabricaConexionSql;
      _configuracion = configuracion;
      _mapeador = mapeador;
    }

    public EmpresaAutenticacion ObtenerDatosToken(string bearerToken)
    {
      var datosToken = Autenticacion.FromJson(bearerToken);
      var empresaAutenticacion = _mapeador.Map<EmpresaAutenticacion>(datosToken.user);
      return empresaAutenticacion;
    }

    public RespuestaConsultarClaveSecreta ConsultarClaveSecretaEmpresaPorId(string idEmpresa)
    {
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarClaveSecretaGestionContribuyentePorId:ProcedimientoAlmacenado"]!;
      DynamicParameters parametros = new();
      parametros.Add("IdEmpresa", idEmpresa);
      var respuesta = conexion.QuerySingleOrDefault<RespuestaConsultarClaveSecreta>(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);
      return (respuesta!);
    }

    public RespuestaValidarClaveSecreta validarClaveSecreta(string claveSecreta, string idEmpresa)
    {
      RespuestaValidarClaveSecreta respuesta = new();
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string consultar = _configuracion["BaseDeDatos:SoloLectura:ValidarClaveSecretaContribuyente:ProcedimientoAlmacenado"]!;
      DynamicParameters parametros = new();
      parametros.Add("ClaveSecreta", claveSecreta);
      parametros.Add("IdEmpresa", idEmpresa);
      parametros.Add(name: "CodigoRespuesta", dbType: DbType.Int32, direction: ParameterDirection.Output);
      parametros.Add(name: "ResultadoRespuesta", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
      parametros.Add(name: "MensajeRespuesta", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

      conexion.Execute(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);

      respuesta.Codigo = parametros.Get<int>("CodigoRespuesta");
      respuesta.Resultado = parametros.Get<string>("ResultadoRespuesta");
      respuesta.Mensaje = parametros.Get<string>("MensajeRespuesta");

      if (respuesta.Codigo == 200)
      {
        respuesta.IdEmpresa = idEmpresa;
      }
      return respuesta;
    }

    public RespuestaConsultarClavesEmpresa ConsultarClavesEmpresaPorIdUsuario(string idEmpresa, string idUsuario)
    {
      RespuestaConsultarClavesEmpresa respuesta = new();
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarClavesEmpresaPorIdUsuario:ProcedimientoAlmacenado"]!;
      int tiempoDeEspera = Convert.ToInt32(_configuracion["BaseDeDatos:EjecucionProcedimientoAlmacenado:TiempoDeEspera"]);
      var parametros = new DynamicParameters();
      parametros.Add("IdEmpresa", idEmpresa);
      parametros.Add("IdUsuario", idUsuario);
      var clavesEmpresa = conexion.QuerySingleOrDefault(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: tiempoDeEspera);

      if (clavesEmpresa != null)
      {
        respuesta.Codigo = 200;
        respuesta.Resultado = "Exitoso";
        respuesta.Mensaje = "Consulta Exitosa";
        respuesta.ClavesEmpresa = JToken.Parse(JsonConvert.SerializeObject(clavesEmpresa));
      }
      else
      {
        respuesta.Codigo = 404;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "No se encontraron datos de la empresa para el usuario";
      }
      return respuesta;
    }
  }
}
