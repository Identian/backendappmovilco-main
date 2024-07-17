using Dapper;
using Dominio.Entidad;
using Dominio.Entidad.Respuestas;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using System.Data;
using Transversal.Comun.Fabricas;

namespace Infraestructura.Repositorio
{
  public class DocumentosRepositorioSql : IDocumentosRepositorioSql
  {
    private readonly IFabricaConexionSql _fabricaConexionSql;
    private readonly IConfiguration _configuracion;

    public DocumentosRepositorioSql(IFabricaConexionSql fabricaConexionSql, IConfiguration configuracion)
    {
      _fabricaConexionSql = fabricaConexionSql;
      _configuracion = configuracion;
    }

    public RespuestaConsultarMontoFacturaPos ConsultarMontoFacturaPos()
    {
      RespuestaConsultarMontoFacturaPos respuesta = new();
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarUVTFacturaPos:ProcedimientoAlmacenado"]!;
      int tiempoDeEspera = Convert.ToInt32(_configuracion["BaseDeDatos:EjecucionProcedimientoAlmacenado:TiempoDeEspera"]);
      var informacionUVT = conexion.QuerySingleOrDefault<UvtFacturaPos>(sql: consultar, commandType: CommandType.StoredProcedure, commandTimeout: tiempoDeEspera);
      if (informacionUVT != null)
      {
        respuesta.Codigo = 200;
        respuesta.Resultado = "Exitoso";
        respuesta.Mensaje = "Consulta Exitosa";
        respuesta.MontoUVT = informacionUVT.MontoUVT;
        respuesta.CantidadUVT = informacionUVT.CantidadUVT;
        respuesta.MontoFacturaPos = informacionUVT.MontoFacturaPos;
        respuesta.FechaVigencia = informacionUVT.FechaVigencia;
      }
      else
      {
        respuesta.Codigo = 404;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "No se encontró información solicitada";
      }
      return respuesta;
    }
  }
}
