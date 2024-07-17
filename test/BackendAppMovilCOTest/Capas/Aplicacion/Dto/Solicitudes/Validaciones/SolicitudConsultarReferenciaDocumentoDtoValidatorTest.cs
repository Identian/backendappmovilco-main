using Aplicacion.Dto.Solicitudes.Validaciones;
using Aplicacion.Entidad.Solicitudes;
using Microsoft.Extensions.Configuration;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudConsultarReferenciaDocumentoDtoValidatorTest
  {
    private SolicitudConsultarReferenciaDocumentoFacturacionDto _solicitudConsultarReferenciaDocumentoDto;
    private SolicitudConsultarReferenciaDocumentoDtoValidator _solicitudConsultarReferenciaDocumentoDtoValidator;

    [SetUp]
    public void Setup()
    {

      Dictionary<string, string> appSettings = new()
      {
        { "ConsultaReferenciaDocumentoTipo:Exitosos", "1" },
        { "ConsultaReferenciaDocumentoTipo:Rechacazdos", "0" },
            };
      IConfiguration configuracion = new ConfigurationBuilder().AddInMemoryCollection(appSettings!).Build();


      _solicitudConsultarReferenciaDocumentoDtoValidator = new(configuracion);
      _solicitudConsultarReferenciaDocumentoDto = new()
      {
        IdEmpresa = "1",
        Nit = "123456789",
        TokenEmpresa = "a123a123a456a456a789a789asd",
        TokenClave = null,
        Plataforma = "TFHKA",
        IdInvoice = "12345",
        TipoConsulta = "1"

      };

    }



    [Test]
    [TestCase("TFHKA", true)]
    [TestCase("DIAN", true)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("OTRA", false)]
    public void Validar_Plataforma(string? plataforma, bool esValido)
    {
      _solicitudConsultarReferenciaDocumentoDto.Plataforma = plataforma;

      var validacion = _solicitudConsultarReferenciaDocumentoDtoValidator.Validate(_solicitudConsultarReferenciaDocumentoDto);

      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }


    [Test]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("a", false)]
    [TestCase("AEFE154", false)]
    [TestCase("1234", true)]
    [TestCase("12.1", false)]
    [TestCase("15", true)]
    public void Validar_Consecutivo(string? consecutivo, bool esValido)
    {
      _solicitudConsultarReferenciaDocumentoDto.IdInvoice = consecutivo;

      var validacion = _solicitudConsultarReferenciaDocumentoDtoValidator.Validate(_solicitudConsultarReferenciaDocumentoDto);

      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("1", true)]
    [TestCase("0", true)]
    [TestCase("3", false)]
    [TestCase("45", false)]
    [TestCase("131dsa", false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("*", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("-1", false)]
    [TestCase("ADef.1", false)]
    [TestCase("adasss", false)]
    public void Validar_Identificador(string? identificador, bool esValido)
    {
      //Arrange
      _solicitudConsultarReferenciaDocumentoDto.TipoConsulta = identificador;

      //Act
      var validacion = _solicitudConsultarReferenciaDocumentoDtoValidator.Validate(_solicitudConsultarReferenciaDocumentoDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

  }
}
