using Aplicacion.Dto.Solicitudes.Validaciones;
using BackendAppMovilCOTest.Capas.Compartida;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes.Establecimiento;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones.Establecimiento
{
  public class SeleccionarEstablecimientoDtoValidatorTest
  {
    SolicitudesCompartidasSeleccionarEstablecimiento _solicitudes = new();
    SolicitudSeleccionarEstablecimientoDtoValidator _solicitudSeleccionarEstablecimientoDtoValidator = new();

    [SetUp]

    //Arrange general
    public void Setup()
    {
      _solicitudes = new();
      _solicitudSeleccionarEstablecimientoDtoValidator = new SolicitudSeleccionarEstablecimientoDtoValidator();
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
    public void Validar_IdEstablecimiento(string? IdEstablecimiento, bool esValido)
    {
      _solicitudes.solicitudSeleccionarEstablecimientoDto.IdEstablecimiento = IdEstablecimiento;

      var validacion = _solicitudSeleccionarEstablecimientoDtoValidator.Validate(_solicitudes.solicitudSeleccionarEstablecimientoDto);

      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }


    [Test]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("a", false)]
    [TestCase("AEFE154", false)]
    [TestCase("1234", false)]
    [TestCase("12.1", false)]
    [TestCase("15", false)]
    [TestCase("1", true)]
    [TestCase("0", true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    public void Validar_Seleccion(string? Seleccion, bool esValido)
    {
      _solicitudes.solicitudSeleccionarEstablecimientoDto.Seleccionado = Seleccion;

      var validacion = _solicitudSeleccionarEstablecimientoDtoValidator.Validate(_solicitudes.solicitudSeleccionarEstablecimientoDto);

      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase("1", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, true)]
    [TestCase(null, true)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho255, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho256, false)]
    public void Validar_Referencia(string? Referencia, bool esValido)
    {
      //Arrange
      _solicitudes.solicitudSeleccionarEstablecimientoDto.Referencia = Referencia;

      var validacion = _solicitudSeleccionarEstablecimientoDtoValidator.Validate(_solicitudes.solicitudSeleccionarEstablecimientoDto);

      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }
  }
}
