using Aplicacion.Dto.Solicitudes.Validaciones.Documentos;
using Aplicacion.Entidad.Documentos;
using BackendAppMovilCOTest.Capas.Compartida;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones.Documentos
{
  public class FacturaImpuestosDtoValidatorTest
  {
    private SolicitudesCompartidasEmitirDocumento _solicitudesCompartidasEmitirDocumento;
    private FacturaImpuestosDto _facturaImpuestosDto;
    private FacturaImpuestosDtoValidator _facturaImpuestosDtoValidator;

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      _solicitudesCompartidasEmitirDocumento = new();
      _facturaImpuestosDto = _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.DetalleDeFactura!.First().ImpuestosDetalles!.First();
      _facturaImpuestosDtoValidator = new();
    }

    [Test]
    [TestCase("-1", true)]
    [TestCase("01", true)]
    [TestCase("12", true)]
    [TestCase("23", true)]
    [TestCase("AB", true)]
    [TestCase("123", false)]
    [TestCase("ABC", false)]
    [TestCase("1", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    [TestCase("0", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_CodigoTOTALImp(string? codigoTOTALImp, bool esValido)
    {
      //Arrange
      _facturaImpuestosDto.CodigoTOTALImp = codigoTOTALImp;

      //Act
      var validacion = _facturaImpuestosDtoValidator.Validate(_facturaImpuestosDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("0", true)]
    [TestCase("1", true)]
    [TestCase("40", true)]
    [TestCase("2.30", true)]
    [TestCase("199.222222", true)]
    [TestCase("199.2", true)]
    [TestCase("1992.2", false)]
    [TestCase("199.2222222", false)]
    [TestCase("1198,345", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorMontoDecimalCadena, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEnteroLargo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.PrecioUnitarioDecimalConAncho19, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("-1", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    public void Validar_PorcentajeTOTALImp(string? porcentajeTOTALImp, bool esValido)
    {
      //Arrange
      _facturaImpuestosDto.PorcentajeTOTALImp = porcentajeTOTALImp;

      //Act
      var validacion = _facturaImpuestosDtoValidator.Validate(_facturaImpuestosDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase("0", true)]
    [TestCase("1", true)]
    [TestCase("40", true)]
    [TestCase("2.30", true)]
    [TestCase("19999999999.2", true)]
    [TestCase("1198,345", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorMontoDecimalCadena, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEnteroLargo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.PrecioUnitarioDecimalConAncho19, false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("-1", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    public void Validar_BaseImponibleTOTALImp(string? baseImponibleTOTALImp, bool esValido)
    {
      //Arrange
      _facturaImpuestosDto.BaseImponibleTOTALImp = baseImponibleTOTALImp;

      //Act
      var validacion = _facturaImpuestosDtoValidator.Validate(_facturaImpuestosDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase("0", true)]
    [TestCase("1", true)]
    [TestCase("40", true)]
    [TestCase("2.30", true)]
    [TestCase("19999999999.2", true)]
    [TestCase("1198,345", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorMontoDecimalCadena, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEnteroLargo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.PrecioUnitarioDecimalConAncho19, false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("-1", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    public void Validar_ValorTOTALImp(string? valorTOTALImp, bool esValido)
    {
      //Arrange
      _facturaImpuestosDto.ValorTOTALImp = valorTOTALImp;

      //Act
      var validacion = _facturaImpuestosDtoValidator.Validate(_facturaImpuestosDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("1", true)]
    [TestCase("2", true)]
    [TestCase("3", true)]
    [TestCase("9", true)]
    [TestCase("10", false)]
    [TestCase("123", false)]
    [TestCase("ABC", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    [TestCase("0", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("-1", false)]
    public void Validar_ControlInterno(string? controlInterno, bool esValido)
    {
      //Arrange
      _facturaImpuestosDto.ControlInterno = controlInterno;

      //Act
      var validacion = _facturaImpuestosDtoValidator.Validate(_facturaImpuestosDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("0", true)]
    [TestCase("1", true)]
    [TestCase("40", true)]
    [TestCase("2.30", true)]
    [TestCase("19999999999.2", true)]
    [TestCase("1198,345", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorMontoDecimalCadena, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEnteroLargo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.PrecioUnitarioDecimalConAncho19, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("-1", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    public void Validar_UnidadMedidaTributo(string? unidadMedidaTributo, bool esValido)
    {
      //Arrange
      _facturaImpuestosDto.UnidadMedidaTributo = unidadMedidaTributo;

      //Act
      var validacion = _facturaImpuestosDtoValidator.Validate(_facturaImpuestosDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("1", true)]
    [TestCase("-1", true)]
    [TestCase("0", true)]
    [TestCase("1.2", true)]
    [TestCase("2,3", true)]
    [TestCase("a", true)]
    [TestCase("Z", true)]
    [TestCase("*", true)]
    [TestCase("123", true)]
    [TestCase("ABC", true)]
    [TestCase("ABCD", false)]
    [TestCase("ABCDE", false)]
    [TestCase("ABCDEF", false)]
    [TestCase("ABCDEFG", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_UnidadMedida(string? unidadMedida, bool esValido)
    {
      //Arrange
      _facturaImpuestosDto.UnidadMedida = unidadMedida;

      //Act
      var validacion = _facturaImpuestosDtoValidator.Validate(_facturaImpuestosDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("0", true)]
    [TestCase("1", true)]
    [TestCase("40", true)]
    [TestCase("2.30", true)]
    [TestCase("19999999999.2", true)]
    [TestCase("1198,345", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorMontoDecimalCadena, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEnteroLargo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.PrecioUnitarioDecimalConAncho19, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("-1", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    public void Validar_ValorTributoUnidad(string? valorTributoUnidad, bool esValido)
    {
      //Arrange
      _facturaImpuestosDto.ValorTributoUnidad = valorTributoUnidad;

      //Act
      var validacion = _facturaImpuestosDtoValidator.Validate(_facturaImpuestosDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }
  }
}
