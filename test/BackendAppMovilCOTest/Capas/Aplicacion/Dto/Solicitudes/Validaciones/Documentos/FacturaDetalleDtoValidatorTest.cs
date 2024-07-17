using Aplicacion.Dto.Solicitudes.Validaciones.Documentos;
using Aplicacion.Entidad.Documentos;
using BackendAppMovilCOTest.Capas.Compartida;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones.Documentos
{
  public class FacturaDetalleDtoValidatorTest
  {
    private SolicitudesCompartidasEmitirDocumento _solicitudesCompartidasEmitirDocumento;
    private FacturaDetalleDto _facturaDetalleDto;
    private FacturaDetalleDto _facturaDetalleIdProductoNullDto;
    private FacturaDetalleDtoValidator _facturaDetalleDtoValidator;

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      _solicitudesCompartidasEmitirDocumento = new();
      _facturaDetalleDto = _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.DetalleDeFactura!.First();
      _facturaDetalleIdProductoNullDto = _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoIdProductoNullDto!.Factura!.DetalleDeFactura!.First();
      _facturaDetalleDtoValidator = new();
    }

    [Test]
    public void EscenarioIdProductoNull_Validar_IdProducto()
    {
      //Arrange

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleIdProductoNullDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(true));
    }

    [Test]
    [TestCase("1", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEnteroLargo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConValorMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, true)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, true)]
    [TestCase("-1", true)]
    [TestCase("0", true)]
    [TestCase("1.2", true)]
    [TestCase("2,3", true)]
    [TestCase("a", true)]
    [TestCase("Z", true)]
    [TestCase("*", true)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51, false)]
    public void EscenarioIdProductoNull_Validar_CodigoProducto(string? codigoProducto, bool esValido)
    {
      //Arrange
      _facturaDetalleIdProductoNullDto.CodigoProducto = codigoProducto;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleIdProductoNullDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase("1", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEnteroLargo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConValorMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, true)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho100, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho255, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho300, true)]
    [TestCase("-1", true)]
    [TestCase("0", true)]
    [TestCase("1.2", true)]
    [TestCase("2,3", true)]
    [TestCase("a", true)]
    [TestCase("Z", true)]
    [TestCase("*", true)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho301, false)]
    public void EscenarioIdProductoNull_Validar_Descripcion(string? Descripcion, bool esValido)
    {
      //Arrange
      _facturaDetalleIdProductoNullDto.Descripcion = Descripcion;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleIdProductoNullDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho20, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, true)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho100, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho255, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho300, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho5000, true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConValorMayorAlMaximo, false)]
    [TestCase("1", false)]
    [TestCase("-1", false)]
    [TestCase("0", false)]
    [TestCase("1.2", false)]
    [TestCase("2,3", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho19, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho5001, false)]
    public void EscenarioIdProductoNull_Validar_Nota(string? nota, bool esValido)
    {
      //Arrange
      _facturaDetalleIdProductoNullDto.Nota = nota;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleIdProductoNullDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("1", true)]
    [TestCase("12", true)]
    [TestCase("23", true)]
    [TestCase("123", true)]
    [TestCase("999", true)]
    [TestCase("1234", false)]
    [TestCase("ABC", false)]
    [TestCase("ABCD", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("-1", false)]
    [TestCase("0", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    public void EscenarioIdProductoNull_Validar_CantidadPorEmpaque(string? cantidadPorEmpaque, bool esValido)
    {
      //Arrange
      _facturaDetalleIdProductoNullDto.CantidadPorEmpaque = cantidadPorEmpaque;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleIdProductoNullDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("1", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEnteroLargo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConValorMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, true)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51, true)]
    [TestCase("-1", true)]
    [TestCase("0", true)]
    [TestCase("1.2", true)]
    [TestCase("2,3", true)]
    [TestCase("a", true)]
    [TestCase("Z", true)]
    [TestCase("*", true)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void EscenarioIdProductoNull_Validar_EstandarCodigoProducto(string? estandarCodigoProducto, bool esValido)
    {
      //Arrange
      _facturaDetalleIdProductoNullDto.EstandarCodigoProducto = estandarCodigoProducto;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleIdProductoNullDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("1", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEnteroLargo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConValorMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConAnchoMayorAlMaximo, true)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, true)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51, true)]
    [TestCase("-1", true)]
    [TestCase("0", true)]
    [TestCase("1.2", true)]
    [TestCase("2,3", true)]
    [TestCase("a", true)]
    [TestCase("Z", true)]
    [TestCase("*", true)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void EscenarioIdProductoNull_Validar_EstandarCodigo(string? estandarCodigo, bool esValido)
    {
      //Arrange
      _facturaDetalleIdProductoNullDto.EstandarCodigo = estandarCodigo;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleIdProductoNullDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase("1", true)]
    [TestCase("123", true)]
    [TestCase("123567", true)]
    [TestCase("999999", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
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
    public void EscenarioIdProductoNull_Validar_CantidadReal(string? cantidadReal, bool esValido)
    {
      //Arrange
      _facturaDetalleIdProductoNullDto.CantidadReal = cantidadReal;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleIdProductoNullDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
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
    public void EscenarioIdProductoNoNull_Validar_IdProducto(string? idProducto, bool esValido)
    {
      //Arrange
      _facturaDetalleDto.IdProducto = idProducto;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleDto);

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
    public void Validar_CantidadUnidades(string? cantidadUnidades, bool esValido)
    {
      //Arrange
      _facturaDetalleDto.CantidadUnidades = cantidadUnidades;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
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
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_UnidadMedida(string? unidadMedida, bool esValido)
    {
      //Arrange
      _facturaDetalleDto.UnidadMedida = unidadMedida;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleDto);

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
    public void Validar_PrecioVentaUnitario(string? precioVentaUnitario, bool esValido)
    {
      //Arrange
      _facturaDetalleDto.PrecioVentaUnitario = precioVentaUnitario;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleDto);

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
    public void Validar_PrecioTotalSinImpuestos(string? precioTotalSinImpuestos, bool esValido)
    {
      //Arrange
      _facturaDetalleDto.PrecioTotalSinImpuestos = precioTotalSinImpuestos;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleDto);

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
    public void Validar_PrecioTotal(string? precioTotal, bool esValido)
    {
      //Arrange
      _facturaDetalleDto.PrecioTotal = precioTotal;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("0", true)]
    [TestCase("1", true)]
    [TestCase("2", false)]
    [TestCase("3", false)]
    [TestCase("40", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("-1", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    public void Validar_MuestraGratis(string? muestraGratis, bool esValido)
    {
      //Arrange
      _facturaDetalleDto.MuestraGratis = muestraGratis;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    public void Validar_ImpuestosDetallesNull()
    {
      //Arrange
      _facturaDetalleDto.ImpuestosDetalles = null;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(true));
    }

    [Test]
    public void Validar_ImpuestosTotalesNull()
    {
      //Arrange
      _facturaDetalleDto.ImpuestosTotales = null;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(true));
    }

    [Test]
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
    [TestCase("ABCD", true)]
    [TestCase("ABCDE", true)]
    [TestCase("ABCDEF", true)]
    [TestCase("ABCDEFG", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_CantidadRealUnidadMedida(string? cantidadRealUnidadMedida, bool esValido)
    {
      //Arrange
      _facturaDetalleDto.CantidadRealUnidadMedida = cantidadRealUnidadMedida;

      //Act
      var validacion = _facturaDetalleDtoValidator.Validate(_facturaDetalleDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }
  }
}
