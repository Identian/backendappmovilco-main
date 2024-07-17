using Aplicacion.Dto.Solicitudes.Validaciones;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes.Dispositivos;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones.Dispositivos
{
  public class DispositivoDtoValidatorTest
  {
    SolicitudesCompartidasDispositivos _solicitudes = new();
    DispositivoDtoValidator _solicitudDispositivoDtoValidator = new();

    [SetUp]

    //Arrange general
    public void Setup()
    {
      _solicitudes = new();
      _solicitudDispositivoDtoValidator = new DispositivoDtoValidator();
    }

    [Test]
    [TestCase("1", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho255, true)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_SerialLogico(string? serialLogico, bool esValido)
    {
      //Arrange
      _solicitudes.DatosValidosCrearDispositivoDto.SerialLogico = serialLogico;

      //Act
      var validacion = _solicitudDispositivoDtoValidator.Validate(_solicitudes.DatosValidosCrearDispositivoDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase("1", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho255, true)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_Nombre(string? nombre, bool esValido)
    {
      //Arrange
      _solicitudes.DatosValidosCrearDispositivoDto.Nombre = nombre;

      //Act
      var validacion = _solicitudDispositivoDtoValidator.Validate(_solicitudes.DatosValidosCrearDispositivoDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase("1", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, true)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_TipoApp(string? tipoApp, bool esValido)
    {
      //Arrange
      _solicitudes.DatosValidosCrearDispositivoDto!.TipoApp = tipoApp;

      //Act
      var validacion = _solicitudDispositivoDtoValidator.Validate(_solicitudes.DatosValidosCrearDispositivoDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }
  }
}
