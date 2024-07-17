using Dapper;
using Dominio.Entidad.Documentos;
using Dominio.Entidad.Respuestas;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using System.Data;
using Transversal.Comun.Fabricas;

namespace Infraestructura.Repositorio
{
  public class ClientesRepositorioSql : IClientesRepositorioSql
  {
    private readonly IFabricaConexionSql _fabricaConexionSql;
    private readonly IConfiguration _configuracion;

    public ClientesRepositorioSql(IFabricaConexionSql fabricaConexionSql, IConfiguration configuracion)
    {
      _fabricaConexionSql = fabricaConexionSql;
      _configuracion = configuracion;
    }

    private IEnumerable<DireccionBase> ConsultarDireccionesClienteDeEmpresaPorId(string idCliente, string idEmpresa)
    {
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string? consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarDireccionesClienteDeEmpresaPorId:ProcedimientoAlmacenado"];
      DynamicParameters parametros = new();
      parametros.Add("IdCliente", idCliente);
      parametros.Add("IdEmpresa", idEmpresa);
      var respuesta = conexion.Query<DireccionBase>(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);
      return (respuesta);
    }

    private IEnumerable<ObligacionesBase> ConsultarResponsabilidadesClienteDeEmpresaPorId(string idCliente, string idEmpresa)
    {
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string? consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarResponsabilidadesClienteDeEmpresaPorId:ProcedimientoAlmacenado"];
      DynamicParameters parametros = new();
      parametros.Add("IdCliente", idCliente);
      parametros.Add("IdEmpresa", idEmpresa);
      var respuesta = conexion.Query<ObligacionesBase>(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);
      return (respuesta);
    }

    private IEnumerable<Tributos> ConsultarDetallesTributariosClienteDeEmpresaPorId(string idCliente, string idEmpresa)
    {
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string? consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarDetallesTributariosClienteDeEmpresaPorId:ProcedimientoAlmacenado"];
      DynamicParameters parametros = new();
      parametros.Add("IdCliente", idCliente);
      parametros.Add("IdEmpresa", idEmpresa);
      var respuesta = conexion.Query<Tributos>(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);
      return (respuesta);
    }

    public static IEnumerable<Destinatario>? ObtenerDestinatario(string? emails, string? separadorEmail, string? canalDeEntrega)
    {
      List<Destinatario>? listaDestinatarios = null;
      if (!string.IsNullOrEmpty(emails))
      {
        List<string> listaEmail = new();
        var emailArray = emails.Split(separadorEmail);
        foreach (var email in emailArray)
        {
          if (!string.IsNullOrEmpty(email))
          {
            listaEmail.Add(email);
          }
        }
        if (listaEmail.Any())
        {
          listaDestinatarios = new() {
            new Destinatario()
            {
              CanalDeEntrega = canalDeEntrega,
              Email = listaEmail
            }
          };
        }
      }
      return (listaDestinatarios);
    }

    public static DireccionBase? ObtenerDireccion(IEnumerable<DireccionBase> direcciones, string tipo)
    {
      DireccionBase direccion = null;
      if ((direcciones != null) && (direcciones.Any()))
      {
        direccion = direcciones.FirstOrDefault(d => d.Tipo == tipo);
      }
      return (direccion);
    }

    public static InformacionLegal ObtenerInformacionLegal(Cliente cliente)
    {
      InformacionLegal informacionLegal = new()
      {
        NombreRegistroRut = cliente.NombreRegistroRut,
        NumeroIdentificacion = cliente.NumeroIdentificacion,
        NumeroIdentificacionDv = cliente.NumeroIdentificacionDv,
        TipoIdentificacion = cliente.TipoIdentificacion
      };
      return (informacionLegal);
    }

    public RespuestaConsultarCliente ConsultarPorId(string idCliente, string idEmpresa)
    {
      RespuestaConsultarCliente respuesta = new();
      using IDbConnection conexion = _fabricaConexionSql.ConexionSoloLectura;
      string? consultar = _configuracion["BaseDeDatos:SoloLectura:ConsultarClienteDeEmpresaPorId:ProcedimientoAlmacenado"];
      DynamicParameters parametros = new();
      parametros.Add("IdCliente", idCliente);
      parametros.Add("IdEmpresa", idEmpresa);
      var cliente = conexion.QuerySingleOrDefault<Cliente>(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);
      if (cliente != null)
      {
        respuesta.Codigo = 200;
        respuesta.Resultado = "Exitoso";
        respuesta.Mensaje = "Consulta Exitosa";
        cliente.Destinatario = ObtenerDestinatario(cliente.Email, _configuracion["ServiciosFacturacion:EmisionRest:Destinatario:SeparadorEmail"], cliente.CanalDeEntrega);
        cliente.ResponsabilidadesRut = ConsultarResponsabilidadesClienteDeEmpresaPorId(idCliente, idEmpresa);
        cliente.DetallesTributarios = ConsultarDetallesTributariosClienteDeEmpresaPorId(idCliente, idEmpresa);
        var direcciones = ConsultarDireccionesClienteDeEmpresaPorId(idCliente, idEmpresa);
        cliente.DireccionCliente = ObtenerDireccion(direcciones, _configuracion["BaseDeDatos:SoloLectura:ConsultarDireccionesClienteDeEmpresaPorId:TipoDireccion:DireccionCliente"]);
        cliente.DireccionFiscal = ObtenerDireccion(direcciones, _configuracion["BaseDeDatos:SoloLectura:ConsultarDireccionesClienteDeEmpresaPorId:TipoDireccion:DireccionFiscal"]);
        cliente.InformacionLegalCliente = ObtenerInformacionLegal(cliente);
        respuesta.Datos = cliente;
      }
      else
      {
        respuesta.Codigo = 404;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "No se encontró el cliente dentro de los registrados a esta empresa";
      }
      return (respuesta);
    }
  }
}
