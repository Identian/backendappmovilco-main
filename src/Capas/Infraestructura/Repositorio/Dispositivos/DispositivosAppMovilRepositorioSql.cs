using Dapper;
using Dominio.Entidad.Dispositivos;
using Dominio.Entidad.Respuestas;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Data;
using Transversal.Comun.Fabricas;
using Transversal.Comun.Respuestas;

namespace Infraestructura.Repositorio.GestionDeAcceso
{
  public class DispositivosAppMovilRepositorioSql : IDispositivosAppMovilRepositorioSql
  {
    private readonly IFabricaConexionSql _fabricaConexionSql;
    private readonly IConfiguration _configuracion;
    private readonly IGestionAccesoAppMovil _gestionAccesoAppMovil;

    public DispositivosAppMovilRepositorioSql(IFabricaConexionSql fabricaConexionSql, IConfiguration configuracion, IGestionAccesoAppMovil gestionAccesoAppMovil)
    {
      _fabricaConexionSql = fabricaConexionSql;
      _configuracion = configuracion;
      _gestionAccesoAppMovil = gestionAccesoAppMovil;
    }

    public JToken? ConsultarNombreYSerialDispositivo(Dispositivo solicitud, string Token)
    {
      JToken respuesta;
      using IDbConnection conexion = _fabricaConexionSql.ConexionLecturaEscritura;
      int tiempoDeEspera = Convert.ToInt32(_configuracion["BaseDeDatos:EjecucionProcedimientoAlmacenado:TiempoDeEspera"]);
      string insertar = _configuracion["BaseDeDatos:SoloLectura:ConsultarSiExisteDispositivo:ProcedimientoAlmacenado"]!;
      int longitudTexto = Convert.ToInt32(_configuracion["BaseDeDatos:SoloLectura:ConsultarSiExisteDispositivo:Parametros:LongitudTexto"]);

      DynamicParameters parametros = new();
      parametros.Add("IdEmpresa", solicitud.IdEmpresa);
      parametros.Add("SerialLogico", solicitud.SerialLogico);
      parametros.Add("Nombre", solicitud.Nombre);
      parametros.Add(name: "CodigoRespuesta", dbType: DbType.Int32, direction: ParameterDirection.Output);
      parametros.Add(name: "ResultadoRespuesta", dbType: DbType.String, direction: ParameterDirection.Output, size: longitudTexto);
      parametros.Add(name: "MensajeRespuesta", dbType: DbType.String, direction: ParameterDirection.Output, size: longitudTexto);
      conexion.Execute(sql: insertar, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: tiempoDeEspera);

      respuesta = JToken.FromObject(new
      {
        Codigo = parametros.Get<int>("CodigoRespuesta"),
        Resultado = parametros.Get<string>("ResultadoRespuesta"),
        Mensaje = parametros.Get<string>("MensajeRespuesta"),
        Nit = (string?)null,
        IdEmpresa = (string?)null,
        IdDispositivo = (string?)null,
        ActivoApp = (string?)null,
        Errores = (string?)null
      });
      if (parametros.Get<int>("CodigoRespuesta") == 200)
      {
        respuesta = _gestionAccesoAppMovil.CrearAccesoDispositivo(solicitud, Token)!;
        return respuesta;
      }
      return (respuesta);
    }

    public RespuestaValidadSubscripcion ValidarSuscripcionDispositivoPorSerialLogico(string idEmpresa, string serialLogico)
    {
      var respuesta = new RespuestaValidadSubscripcion();
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      int tiempoDeEspera = Convert.ToInt32(_configuracion["BaseDeDatos:EjecucionProcedimientoAlmacenado:TiempoDeEspera"]);
      string consultar = _configuracion["BaseDeDatos:SoloLectura:ValidarSuscripcionDispositivoPorSerialLogico:ProcedimientoAlmacenado"]!;
      int longitudTexto = Convert.ToInt32(_configuracion["BaseDeDatos:SoloLectura:ValidarSuscripcionDispositivoPorSerialLogico:Parametros:LongitudTexto"]);

      DynamicParameters parametros = new();
      parametros.Add("IdEmpresa", idEmpresa);
      parametros.Add("SerialLogico", serialLogico);
      parametros.Add(name: "CodigoRespuesta", dbType: DbType.Int32, direction: ParameterDirection.Output);
      parametros.Add(name: "ResultadoRespuesta", dbType: DbType.String, direction: ParameterDirection.Output, size: longitudTexto);
      parametros.Add(name: "MensajeRespuesta", dbType: DbType.String, direction: ParameterDirection.Output, size: longitudTexto);
      parametros.Add(name: "@IdSuscripcion", dbType: DbType.String, direction: ParameterDirection.Output, size: longitudTexto);
      conexion.Execute(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: tiempoDeEspera);
      respuesta.Codigo = parametros.Get<int>("CodigoRespuesta");
      respuesta.Resultado = parametros.Get<string>("ResultadoRespuesta");
      respuesta.Mensaje = parametros.Get<string>("MensajeRespuesta");
      respuesta.IdSuscripcion = parametros.Get<string?>("@IdSuscripcion") ?? "0";
      return (respuesta);
    }

    public JToken? ConsultarSuscripcionDispositivoPorSerialLogico(string idEmpresa, string serialLogico)
    {
      JToken respuesta;
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      int tiempoDeEspera = Convert.ToInt32(_configuracion["BaseDeDatos:EjecucionProcedimientoAlmacenado:TiempoDeEspera"]);
      string consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarSuscripcionDispositivoPorSerialLogico:ProcedimientoAlmacenado"]!;
      int longitudTexto = Convert.ToInt32(_configuracion["BaseDeDatos:SoloLectura:ConsultarSuscripcionDispositivoPorSerialLogico:Parametros:LongitudTexto"]);

      DynamicParameters parametros = new();
      parametros.Add("IdEmpresa", idEmpresa);
      parametros.Add("SerialLogico", serialLogico);
      parametros.Add(name: "CodigoRespuesta", dbType: DbType.Int32, direction: ParameterDirection.Output);
      parametros.Add(name: "ResultadoRespuesta", dbType: DbType.String, direction: ParameterDirection.Output, size: longitudTexto);
      parametros.Add(name: "MensajeRespuesta", dbType: DbType.String, direction: ParameterDirection.Output, size: longitudTexto);
      parametros.Add(name: "IdSuscripcion", dbType: DbType.Int32, direction: ParameterDirection.Output);
      parametros.Add(name: "SerialSuscripcion", dbType: DbType.String, direction: ParameterDirection.Output, size: longitudTexto);
      conexion.Execute(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: tiempoDeEspera);

      respuesta = JToken.FromObject(new
      {
        Codigo = parametros.Get<int>("CodigoRespuesta"),
        Resultado = parametros.Get<string>("ResultadoRespuesta"),
        Mensaje = parametros.Get<string>("MensajeRespuesta"),
        IdSuscripcion = parametros.Get<int?>("IdSuscripcion"),
        SerialSuscripcion = parametros.Get<string?>("SerialSuscripcion")
      });
      return (respuesta);
    }
  }
}
