using Dapper;
using Dominio.Entidad.Empresas;
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
  public class SecuencialesRepositorio : ISecuencialesRepositorio
  {
    private readonly IFabricaConexionSql _fabricaConexionSql;
    private readonly IConfiguration _configuracion;
    public SecuencialesRepositorio(IConfiguration configuracion, IFabricaConexionSql fabricaConexionSql)
    {
      _configuracion = configuracion;
      _fabricaConexionSql = fabricaConexionSql;
    }

    public RespuestaConsultarNumeraciones ConsultarNumeraciones(SolicitudConsultarFacturacion solicitudConsultarNumeraciones)
    {
      RespuestaConsultarNumeraciones respuesta = new();
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:GestionContribuyentes:Url"]!);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:GestionContribuyentes:ApiConsultarNumeraciones"]);
      solicitudRest.Method = Method.Post;
      solicitudRest.RequestFormat = DataFormat.Json;
      solicitudRest.AddHeader("Content-type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      solicitudRest.AddJsonBody(JsonConvert.SerializeObject(solicitudConsultarNumeraciones));
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful)
      {
        respuesta = JsonConvert.DeserializeObject<RespuestaConsultarNumeraciones>(respuestaRest.Content!)!;
      }
      else
      {
        respuesta.Codigo = Convert.ToInt32(respuestaRest.StatusCode);
        respuesta.Mensaje = string.IsNullOrEmpty(respuestaRest.ErrorMessage) ? respuestaRest.ErrorException!.Message : respuestaRest.ErrorMessage;
        respuesta.Resultado = "Error";
      }
      return respuesta;

    }

    public RespuestaSeleccionarSecuencial Seleccionar(SolicitudSeleccionarSecuencial solicitud, EmpresaAutenticacion datosToken)
    {
      RespuestaSeleccionarSecuencial respuesta = new();

      using IDbConnection conexion = _fabricaConexionSql.ConexionLecturaEscritura;
      int tiempoDeEspera = Convert.ToInt32(_configuracion["BaseDeDatos:EjecucionProcedimientoAlmacenado:TiempoDeEspera"]);
      string insertar = _configuracion["BaseDeDatos:LecturaEscritura:SeleccionarSecuencial:ProcedimientoAlmacenado"]!;
      int longitudTexto = Convert.ToInt32(_configuracion["BaseDeDatos:LecturaEscritura:SeleccionarSecuencial:Parametros:LongitudTexto"]);
      DynamicParameters parametros = new();
      parametros.Add("IdEmpresa", datosToken.IdEmpresa);
      parametros.Add("IdNumeracion", solicitud.IdNumeracion);
      parametros.Add("Seleccionado", solicitud.Seleccionado);
      parametros.Add("Referencia", solicitud.Referencia);
      parametros.Add(name: "Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
      parametros.Add(name: "Resultado", dbType: DbType.String, direction: ParameterDirection.Output, size: longitudTexto);
      parametros.Add(name: "Mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: longitudTexto);
      conexion.Execute(sql: insertar, param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: tiempoDeEspera);
      respuesta.Codigo = parametros.Get<int>("Codigo");
      respuesta.Resultado = parametros.Get<string>("Resultado");
      respuesta.Mensaje = parametros.Get<string>("Mensaje");
      if (respuesta.Codigo == 200)
      {
        respuesta.Nit = datosToken.NitEmpresa;
        respuesta.IdEmpresa = datosToken.IdEmpresa;
        respuesta.IdNumeracion = solicitud.IdNumeracion;

      }
      return (respuesta);
    }
  }
}



