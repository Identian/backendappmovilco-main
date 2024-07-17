using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;
using BackendAppMovilCOTest.Capas.Compartida;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudEmitirDocumentoDtoValidatorTest
  {
    private SolicitudesCompartidasEmitirDocumento _solicitudesCompartidasEmitirDocumento;
    private SolicitudEmitirDocumentoDto _solicitudEmitirDocumentoDto;
    private SolicitudEmitirDocumentoDtoValidator _solicitudEmitirDocumentoDtoValidator;

    [SetUp]
    public void Setup()
    {
      //Arrange general
      _solicitudesCompartidasEmitirDocumento = new();
      _solicitudEmitirDocumentoDto = _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!;
      _solicitudEmitirDocumentoDtoValidator = new();
    }

    [Test]
    [TestCase("1", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("-1", false)]
    [TestCase("0", false)]
    [TestCase("1.2", false)]
    [TestCase("2,3", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    public void Validar_IdRangoNumeracion(string? idRangoNumeracion, bool esValido)
    {
      //Arrange
      _solicitudEmitirDocumentoDto.Factura!.IdRangoNumeracion = idRangoNumeracion;

      //Act
      var validacion = _solicitudEmitirDocumentoDtoValidator.Validate(_solicitudEmitirDocumentoDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase("1", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase(null, true)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("-1", false)]
    [TestCase("0", false)]
    [TestCase("1.2", false)]
    [TestCase("2,3", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    [TestCase("00", true)]
    public void Validar_IdCliente(string? idCliente, bool esValido)
    {
      //Arrange
      _solicitudEmitirDocumentoDto.Factura!.Cliente!.IdCliente = idCliente;

      //Act
      var validacion = _solicitudEmitirDocumentoDtoValidator.Validate(_solicitudEmitirDocumentoDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("1", true)]
    [TestCase("2", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("-1", false)]
    [TestCase("0", false)]
    [TestCase("1.2", false)]
    [TestCase("2,3", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    [TestCase("00", false)]
    public void Validar_TipoApp(string? tipoApp, bool esValido)
    {
      //Arrange
      _solicitudEmitirDocumentoDto.TipoApp = tipoApp;

      //Act
      var validacion = _solicitudEmitirDocumentoDtoValidator.Validate(_solicitudEmitirDocumentoDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("a", true)]
    [TestCase("Z", true)]
    [TestCase("*", true)]
    [TestCase("00", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho255, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho256, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_SerialLogico(string? serialLogico, bool esValido)
    {
      //Arrange
      _solicitudEmitirDocumentoDto.SerialLogico = serialLogico;

      //Act
      var validacion = _solicitudEmitirDocumentoDtoValidator.Validate(_solicitudEmitirDocumentoDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }
  }
}
