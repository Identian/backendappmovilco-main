using Dapper;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Data;
using Transversal.Comun.Fabricas;

namespace Infraestructura.Repositorio
{
  public class UsuarioAutenticacionRepositorio : IUsuarioAutenticacionRepositorio
  {
    private readonly IFabricaConexionSql _fabricaConexionSql;
    private readonly IConfiguration _configuracion;

    public UsuarioAutenticacionRepositorio(IFabricaConexionSql fabricaConexionSql, IConfiguration configuracion)
    {
      _fabricaConexionSql = fabricaConexionSql;
      _configuracion = configuracion;
    }

    public static TimeSpan TiempoExpiracionToken(string fechaHoraExpiracionOriginal)
    {
      DateTime ahora = DateTime.UtcNow;
      DateTime fechaHoraExpiracionUtc = Convert.ToDateTime(fechaHoraExpiracionOriginal.Replace("Z", "").Replace("T", " "));
      TimeSpan tiempo = fechaHoraExpiracionUtc - ahora;
      return (tiempo);
    }

    private DynamicParameters PrepararParametros(UsuarioAutenticacion usuarioAutenticacion)
    {
      var resultado = new DynamicParameters();
      var propiedades = typeof(UsuarioAutenticacion).GetProperties();
      for (int i = 0; i < propiedades.Length; i++)
      {
        if (propiedades[i].Name != "TipoApp")
        {
          resultado.Add(propiedades[i].Name, propiedades[i].GetValue(usuarioAutenticacion));
        }
      }
      return resultado;
    }

    public RespuestaValidarCredenciales ValidarCredenciales(UsuarioAutenticacion usuarioAutenticacion)
    {
      var resultado = new RespuestaValidarCredenciales();
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string consultar = _configuracion["BaseDeDatos:SoloLectura:ValidarUsuario:ProcedimientoAlmacenado"]!;
      int longitudMensaje = Convert.ToInt32(_configuracion["BaseDeDatos:SoloLectura:ValidarUsuario:Parametros:LongitudMensaje"]);
      var parametros = PrepararParametros(usuarioAutenticacion);
      parametros.Add(name: "Resultado", dbType: DbType.Boolean, direction: ParameterDirection.Output);
      parametros.Add(name: "Mensaje", dbType: DbType.String, size: longitudMensaje, direction: ParameterDirection.Output);
      parametros.Add(name: "IdUsuario", dbType: DbType.Int32, direction: ParameterDirection.Output);
      conexion.Execute(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);
      resultado.Resultado = parametros.Get<bool>("Resultado");
      resultado.Mensaje = parametros.Get<string>("Mensaje");
      resultado.IdUsuario = resultado.Resultado ? parametros.Get<int>("IdUsuario") : 0;
      return resultado;
    }

    public RespuestaObtenerTokensEmpresa ObtenerTokensEmpresa(int idUsuario)
    {
      var resultado = new RespuestaObtenerTokensEmpresa();
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string consultar = _configuracion["BaseDeDatos:SoloLectura:ObtenerTokensEnterprise:ProcedimientoAlmacenado"]!;
      int longitudMensaje = Convert.ToInt32(_configuracion["BaseDeDatos:SoloLectura:ObtenerTokensEnterprise:Parametros:LongitudMensaje"]);
      int longitudTokenEnterprise = Convert.ToInt32(_configuracion["BaseDeDatos:SoloLectura:ObtenerTokensEnterprise:Parametros:LongitudTokenEnterprise"]);
      int longitudTokenPassword = Convert.ToInt32(_configuracion["BaseDeDatos:SoloLectura:ObtenerTokensEnterprise:Parametros:LongitudTokenPassword"]);
      var parametros = new DynamicParameters();
      parametros.Add(name: "IdUsuario", value: idUsuario, dbType: DbType.Int32, direction: ParameterDirection.Input);
      parametros.Add(name: "Resultado", dbType: DbType.Boolean, direction: ParameterDirection.Output);
      parametros.Add(name: "Mensaje", dbType: DbType.String, size: longitudMensaje, direction: ParameterDirection.Output);
      parametros.Add(name: "TokenEnterprise", dbType: DbType.String, size: longitudTokenEnterprise, direction: ParameterDirection.Output);
      parametros.Add(name: "TokenPassword", dbType: DbType.String, size: longitudTokenPassword, direction: ParameterDirection.Output);
      conexion.Execute(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);
      resultado.Resultado = parametros.Get<bool>("Resultado");
      resultado.Mensaje = parametros.Get<string>("Mensaje");
      resultado.TokenEnterprise = parametros.Get<string>("TokenEnterprise");
      resultado.TokenPassword = parametros.Get<string>("TokenPassword");
      return resultado;
    }

    public RespuestaIniciarSesion UsuarioLoginSoap(string tokenEnterprise, string tokenPassword)
    {
      RespuestaIniciarSesion respuesta = new();
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:LoginSoap:Url"]!);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:LoginSoap:Api"]);
      solicitudRest.Method = Method.Post;
      solicitudRest.RequestFormat = DataFormat.Json;
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      solicitudRest.AddJsonBody(new { user = tokenEnterprise, password = tokenPassword });
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful && respuestaRest.StatusCode == System.Net.HttpStatusCode.OK)
      {
        respuesta = JsonConvert.DeserializeObject<RespuestaIniciarSesion>(respuestaRest.Content!)!;
      }
      return (respuesta);
    }

    public RespuestaIniciarSesion AutenticarUsuario(UsuarioAutenticacion usuarioAutenticacion)
    {
      var resultado = new RespuestaIniciarSesion();
      resultado.response = "error";
      //Validar credenciales de usuario
      var respuestaValidacion = ValidarCredenciales(usuarioAutenticacion);
      if (respuestaValidacion.Resultado)
      {
        //Obtener tokens de la empresa
        var respuestaTokens = ObtenerTokensEmpresa(respuestaValidacion.IdUsuario);
        if (respuestaTokens.Resultado)
        {
          //Consumir servicio Login Soap para obtener Bearer Token
          var respuestaLogin = UsuarioLoginSoap(respuestaTokens.TokenEnterprise!, respuestaTokens.TokenPassword!);
          if (!string.IsNullOrEmpty(respuestaLogin.token))
          {
            resultado.Codigo = 200;
            resultado.response = "success";
            resultado.message = "Inicio de Sesión exitoso.";
            resultado.token = respuestaLogin.token;
            resultado.passwordExpiration = respuestaLogin.passwordExpiration;
            resultado.TiempoExpiracion = TiempoExpiracionToken(respuestaLogin.passwordExpiration!);
          }
          else
          {
            resultado.Codigo = 401;
            resultado.message = "Tokens inválidos";
          }
        }
        else
        {
          resultado.Codigo = 401;
          resultado.message = respuestaTokens.Mensaje;
        }
      }
      else
      {
        resultado.Codigo = 401;
        resultado.message = respuestaValidacion.Mensaje;
      }
      return resultado;
    }
  }
}