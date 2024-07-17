using Dapper;
using Dominio.Entidad.Empresas;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Data;
using Transversal.Comun.Fabricas;

namespace Infraestructura.Repositorio
{
  public class EstablecimientosRepositorio : IEstablecimientosRepositorio
  {
    private readonly IFabricaConexionSql _fabricaConexionSql;
    private readonly IConfiguration _configuracion;

    public EstablecimientosRepositorio(IFabricaConexionSql fabricaConexionSql, IConfiguration configuracion)
    {
      _fabricaConexionSql = fabricaConexionSql;
      _configuracion = configuracion;
    }
    public RespuestaSeleccionarEstablecimiento Seleccionar(SolicitudSeleccionarEstablecimiento solicitud, EmpresaAutenticacion datostoken)
    {
      RespuestaSeleccionarEstablecimiento respuesta = new();
      if (solicitud.Referencia == null)
      {
        solicitud.Referencia = "";
      }
      using IDbConnection conexion = _fabricaConexionSql.ConexionLecturaEscritura;
      int tiempoDeEspera = Convert.ToInt32(_configuracion["BaseDeDatos:EjecucionProcedimientoAlmacenado:TiempoDeEspera"]);
      string insertar = _configuracion["BaseDeDatos:LecturaEscritura:SeleccionarEstablecimiento:ProcedimientoAlmacenado"]!;
      int longitudTexto = Convert.ToInt32(_configuracion["BaseDeDatos:LecturaEscritura:SeleccionarEstablecimiento:Parametros:LongitudTexto"]);
      DynamicParameters parametros = new();
      parametros.Add("IdEmpresa", datostoken.IdEmpresa);
      parametros.Add("IdEstablecimiento", solicitud.IdEstablecimiento);
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
        respuesta.Nit = datostoken.NitEmpresa;
        respuesta.IdEmpresa = datostoken.IdEmpresa;
        respuesta.IdEstablecimiento = solicitud.IdEstablecimiento;

      }
      return (respuesta);
    }
  }
}
