using Aplicacion.Dto.Solicitudes.Validaciones.Documentos;
using Aplicacion.Entidad.Documentos;
using BackendAppMovilCOTest.Capas.Compartida;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones.Documentos
{
  public class FacturaGeneralDtoValidatorTest
  {
    private SolicitudesCompartidasEmitirDocumento _solicitudesCompartidasEmitirDocumento;
    private FacturaGeneralDto _facturaGeneralDto;
    private FacturaGeneralDtoValidator _facturaGeneralDtoValidator;

    [SetUp]
    public void Setup()
    {
      //Arrange general
      _solicitudesCompartidasEmitirDocumento = new();
      _facturaGeneralDto = _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!;
      _facturaGeneralDtoValidator = new();
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
      _facturaGeneralDto.IdRangoNumeracion = idRangoNumeracion;

      //Act
      var validacion = _facturaGeneralDtoValidator.Validate(_facturaGeneralDto);

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
    public void Validar_IdCliente(string? idCliente, bool esValido)
    {
      //Arrange
      _facturaGeneralDto.Cliente!.IdCliente = idCliente;

      //Act
      var validacion = _facturaGeneralDtoValidator.Validate(_facturaGeneralDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }
  }
}
