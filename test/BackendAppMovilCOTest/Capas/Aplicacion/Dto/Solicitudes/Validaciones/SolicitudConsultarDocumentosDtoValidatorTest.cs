using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudConsultarDocumentosDtoValidatorTest
  {
    private SolicitudConsultarDocumentosDto _solicitudConsultarDocumentosDto;
    private SolicitudConsultarDocumentosDtoValidator _solicitudConsultarDocumentosDtoValidator;

    [SetUp]
    public void SetUp()
    {
      _solicitudConsultarDocumentosDto = new()
      {
        Sistema = "1",
        FormatoRequerido = "json"
      };
      _solicitudConsultarDocumentosDtoValidator = new();
    }

    [Test]
    [TestCase(null, true)] //Facturación (valor por defecto)
    [TestCase("1", true)] //Facturación
    [TestCase("2", true)] //Recepción
    [TestCase("3", true)] //Nómina
    [TestCase("-1", false)]
    [TestCase("0", false)]
    [TestCase("4", false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_Sistema(string? sistema, bool esValido)
    {
      //Arrange
      _solicitudConsultarDocumentosDto.Sistema = sistema;

      //Act
      var validacion = _solicitudConsultarDocumentosDtoValidator.Validate(_solicitudConsultarDocumentosDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("csv", true)]
    [TestCase("json", true)]
    [TestCase("CSV", false)]
    [TestCase("JSON", false)]
    [TestCase("txt", false)]
    [TestCase("xls", false)]
    [TestCase("rpt", false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_FormatoRequerido(string? formatoRequerido, bool esValido)
    {
      //Arrange
      _solicitudConsultarDocumentosDto.FormatoRequerido = formatoRequerido;

      //Act
      var validacion = _solicitudConsultarDocumentosDtoValidator.Validate(_solicitudConsultarDocumentosDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }
  }
}
