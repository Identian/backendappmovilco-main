using Aplicacion.Dto.Solicitudes.Validaciones;
using BackendAppMovilCOTest.Capas.Compartida;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones
{
  public class FiltrosTotalDocumentosDtoValidatorTest
  {
    private SolicitudesCompartidasIndicadores _solicitudes;
    private FiltrosTotalDocumentosDtoValidator _filtrosTotalDocumentosDtoValidator;
    private AppSettingsMock _appSettingsMock;

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      _solicitudes = new();
      _appSettingsMock = new();
      _filtrosTotalDocumentosDtoValidator = new(_appSettingsMock.Object);
    }

    [Test]
    [TestCase("Cantidad", true)]
    [TestCase("Monto", true)]
    [TestCase(null, true)]
    [TestCase("", false)]
    [TestCase("1", false)]
    [TestCase("123", false)]
    [TestCase("Total", false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, false)]
    public void Validate_Tipo(string? tipo, bool esValido)
    {
      //Arrange
      _solicitudes.DatosExistentesConsultarTotalDocumentosDto!.Tipo = tipo;

      //Act
      var validate = _filtrosTotalDocumentosDtoValidator.Validate(_solicitudes.DatosExistentesConsultarTotalDocumentosDto);

      //Assert
      Assert.That(validate.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("2000", true)]
    [TestCase("2022", true)]
    [TestCase("2023", true)]
    [TestCase("2099", true)]
    [TestCase("2100", false)]
    [TestCase("1999", false)]
    [TestCase("199", false)]
    [TestCase("19", false)]
    [TestCase("1", false)]
    [TestCase("*/-", false)]
    [TestCase("-1", false)]
    [TestCase("0", false)]
    [TestCase("1.2", false)]
    [TestCase("2,3", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEnteroLargo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, false)]
    public void Validate_Anio(string? anio, bool esValido)
    {
      //Arrange
      _solicitudes.DatosExistentesConsultarTotalDocumentosDto!.Anio = anio;

      //Act
      var validate = _filtrosTotalDocumentosDtoValidator.Validate(_solicitudes.DatosExistentesConsultarTotalDocumentosDto);

      //Assert
      Assert.That(validate.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("1", true)]
    [TestCase("2", true)]
    [TestCase("3", true)]
    [TestCase("4", false)]
    [TestCase("0", false)]
    [TestCase("-1", false)]
    [TestCase("1.2", false)]
    [TestCase("2,3", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51, false)]
    public void Validar_OrigenFacturacionIndividual(string? origenFacturacion, bool esValido)
    {
      //Arrange
      _solicitudes.DatosExistentesConsultarTotalDocumentosDto!.OrigenFacturacion = origenFacturacion != null ? new string[] { origenFacturacion } : null;

      //Act
      var validate = _filtrosTotalDocumentosDtoValidator.Validate(_solicitudes.DatosExistentesConsultarTotalDocumentosDto);

      //Assert
      Assert.That(validate.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase(new string[] { "1" }, true)]
    [TestCase(new string[] { "1", "2" }, true)]
    [TestCase(new string[] { "1", "2", "3" }, true)]
    [TestCase(new string[] { "1", "2", "3", "4" }, false)]
    [TestCase(new string[] { "1", "2", "3", "0" }, false)]
    [TestCase(new string[] { "", "2", "3" }, false)]
    [TestCase(new string[] { "a", "", " " }, false)]
    [TestCase(new string[] { " ", "", " " }, false)]
    [TestCase(new string[] { "a", "a", "a" }, false)]
    public void Validar_OrigenFacturacionArray(string[]? origenFacturacion, bool esValido)
    {
      //Arrange
      _solicitudes.DatosExistentesConsultarTotalDocumentosDto!.OrigenFacturacion = origenFacturacion;

      //Act
      var validate = _filtrosTotalDocumentosDtoValidator.Validate(_solicitudes.DatosExistentesConsultarTotalDocumentosDto);

      //Assert
      Assert.That(validate.IsValid, Is.EqualTo(esValido));
    }
  }
}
