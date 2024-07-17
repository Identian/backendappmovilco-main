using Aplicacion.Dto.Solicitudes.Validaciones.Documentos;
using Aplicacion.Entidad.Documentos;
using BackendAppMovilCOTest.Capas.Compartida;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones.Documentos
{
  public class ImpuestosTotalesDtoValidatorTest
  {
    private SolicitudesCompartidasEmitirDocumento _solicitudesCompartidasEmitirDocumento;
    private ImpuestosTotalesDto _impuestosTotalesDto;
    private ImpuestosTotalesDtoValidator _impuestosTotalesDtoValidator;

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      _solicitudesCompartidasEmitirDocumento = new();
      _impuestosTotalesDto = _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.DetalleDeFactura!.First().ImpuestosTotales!.First();
      _impuestosTotalesDtoValidator = new();
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
      _impuestosTotalesDto.CodigoTOTALImp = codigoTOTALImp;

      //Act
      var validacion = _impuestosTotalesDtoValidator.Validate(_impuestosTotalesDto);

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
    public void Validar_MontoTotal(string? montoTotal, bool esValido)
    {
      //Arrange
      _impuestosTotalesDto.MontoTotal = montoTotal;

      //Act
      var validacion = _impuestosTotalesDtoValidator.Validate(_impuestosTotalesDto);

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
    public void Validar_RedondeoAplicado(string? redondeoAplicado, bool esValido)
    {
      //Arrange
      _impuestosTotalesDto.RedondeoAplicado = redondeoAplicado;

      //Act
      var validacion = _impuestosTotalesDtoValidator.Validate(_impuestosTotalesDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }
  }
}
