using BackendAppMovilCOTest.Capas.Compartida;
using Infraestructura.Repositorio;
using Microsoft.Extensions.Configuration;

namespace BackendAppMovilCOTest.Capas.Infraestructura.Repositorio
{
  public class ClientesRepositorioApiTest
  {
    private IConfiguration _configuracion;
    private readonly RespuestasCompartidasClientes _respuestasCompartidasClientes = new();

    [SetUp]
    public void SetUp()
    {
      //Arrange General
      Dictionary<string, string> appSettings = new()
      {
        { "ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente:NombreRazonSocial", "consumidor final" },
        { "ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente:TipoPersona", "1" },
        { "ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente:TipoIdentificacion", "13" },
        { "ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente:NumeroDocumento", "222222222222" },
        { "ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente:NumeroIdentificacionDV", null },
        { "ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente:Notificar", "NO" },
        { "ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente:Destinatario", null },
        { "ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente:ResponsabilidadesRut:0:Obligaciones", "R-99-PN" },
        { "ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente:DetallesTributarios:0:CodigoImpuesto", "ZZ" },
        { "ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente:DireccionCliente", null },
        { "ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente:DireccionFiscal", null },
        { "ServiciosFacturacion:EmisionRest:ClienteFinalEscenarioNull:Cliente:InformacionLegalCliente", null }
      };
      _configuracion = new ConfigurationBuilder().AddInMemoryCollection(appSettings!).Build();
    }

    [Test]
    public void ObtenerInformacionClienteEscenarioNull()
    {
      //Arrange
      ClientesRepositorioApi clientesRepositorioApi = new(_configuracion);

      //Act
      var cliente = clientesRepositorioApi.ObtenerInformacionClienteEscenarioNull();

      //Assert
      cliente.Should().BeEquivalentTo(_respuestasCompartidasClientes.ClienteFinalEscenarioNull);
    }
  }
}
