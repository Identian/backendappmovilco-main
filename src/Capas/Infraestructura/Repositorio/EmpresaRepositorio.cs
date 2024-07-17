using Dapper;
using Dominio.Entidad.Empresas;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Data;
using Transversal.Comun.Fabricas;
using Transversal.Comun.Respuestas;

namespace Infraestructura.Repositorio
{
  public class EmpresaRepositorio : IEmpresaRepositorio
  {
    private readonly IFabricaConexionSql _fabricaConexionSql;
    private readonly IConfiguration _configuracion;

    public EmpresaRepositorio(IFabricaConexionSql fabricaConexionSql, IConfiguration configuracion)
    {
      _fabricaConexionSql = fabricaConexionSql;
      _configuracion = configuracion;
    }

    public JToken ConsultarEmpresa(SolicitudConsultarFacturacion solicitud)
    {
      JToken respuesta;
      var clienteRest = new RestClient(_configuracion["ServiciosFacturacion:GestionContribuyentes:Url"]!);
      var solicitudRest = new RestRequest(_configuracion["ServiciosFacturacion:GestionContribuyentes:ApiConsultarEmpresa"])
      {
        Method = Method.Post,
        RequestFormat = DataFormat.Json
      };
      solicitudRest.AddHeader("Content-Type", "application/json");
      solicitudRest.AddHeader("Accept", "application/json");
      solicitudRest.AddJsonBody(JsonConvert.SerializeObject(solicitud));
      var respuestaRest = clienteRest.Execute(solicitudRest);
      if (respuestaRest.IsSuccessful)
      {
        respuesta = JToken.Parse(respuestaRest.Content!);
      }
      else
      {
        respuesta = JToken.FromObject(RespuestaBase.Error500("Infraestructura", string.IsNullOrEmpty(respuestaRest.ErrorMessage) ? respuestaRest.ErrorException!.Message : respuestaRest.ErrorMessage));
      }
      return respuesta;
    }

    public RespuestaConsultarEmpresaFacturacion ConsultarEmpresaPorId(int idEmpresa)
    {
      RespuestaConsultarEmpresaFacturacion respuesta = new();
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarEmpresaPorId:ProcedimientoAlmacenado"]!;
      DynamicParameters parametros = new();
      parametros.Add(name: "IdEmpresa", value: idEmpresa, dbType: DbType.Int32, direction: ParameterDirection.Input);
      var empresa = conexion.QuerySingleOrDefault<EmpresaFacturacion>(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);
      if (empresa != null)
      {
        respuesta.Codigo = 200;
        respuesta.Resultado = "Procesado";
        respuesta.Mensaje = "Consulta Exitosa";
        respuesta.Contribuyente = empresa;
      }
      else
      {
        respuesta.Codigo = 404;
        respuesta.Resultado = "Procesado";
        respuesta.Mensaje = "No se encontro la empresa con el id solicitado";
      }
      return respuesta;
    }
  }
}
