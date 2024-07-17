using Dominio.Entidad.Documentos;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Infraestructura.Repositorio
{
  public class ClientesRepositorioApi : IClientesRepositorioApi
  {
    private readonly IConfiguration _configuracion;

    public ClientesRepositorioApi(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }



    public Cliente ObtenerInformacionCliente(int idCliente)
    {
      return new Cliente
      {
        NombreRazonSocial = "The Factory HKA Colombia",
        TipoPersona = "1",
        TipoIdentificacion = "31",
        NumeroDocumento = "901041710",
        NumeroIdentificacionDv = "5",
        Email = "prueba@thefactoryhka.com",
        Notificar = "SI",
        ResponsabilidadesRut = new List<ObligacionesBase>
        {
          new ObligacionesBase
          {
            Obligaciones = "0-14",
            Regimen = "04"
          }
        },
        DetallesTributarios = new List<Tributos>
        {
          new Tributos
          {
            CodigoImpuesto = "01"
          }
        },
        DireccionFiscal = new DireccionBase
        {
          Ciudad = "MANIZALES",
          CodigoDepartamento = "11",
          Departamento = "Bogotá",
          Direccion = "Direccion",
          Lenguaje = "es",
          Municipio = "11001",
          Pais = "CO",
          ZonaPostal = "110211"
        },
        InformacionLegalCliente = new InformacionLegal
        {
          CodigoEstablecimiento = "00001",
          NombreRegistroRut = "CONSORCIO ALIANZA SAN CRISTOBAL 4",
          NumeroIdentificacion = "901041710",
          NumeroIdentificacionDv = "5",
          TipoIdentificacion = "31"
        }
      };
    }

    public Cliente ObtenerInformacionClienteEscenarioNull()
    {
      return _configuracion.GetSection("ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente").Get<Cliente>()!;
    }

  }
}
