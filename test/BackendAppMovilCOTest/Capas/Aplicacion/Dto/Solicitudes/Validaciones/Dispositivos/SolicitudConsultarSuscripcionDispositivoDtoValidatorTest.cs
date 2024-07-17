using Aplicacion.Dto.Solicitudes.Validaciones.Dispositivos;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes.Dispositivos;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones.Dispositivos
{
  public class SolicitudConsultarSuscripcionDispositivoDtoValidatorTest
  {
    private SolicitudesCompartidasDispositivos _solicitudes;
    private SolicitudConsultarSuscripcionDispositivoDtoValidator _validator;

    [SetUp]
    public void Setup()
    {
      //Arrange general
      _solicitudes = new();
      _validator = new();
    }

    [Test]
    [TestCase("1", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho255, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho256, false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_SerialLogico(string? serialLogico, bool esValido)
    {
      //Arrange
      _solicitudes.ConsultarSuscripcionDispositivoDto.SerialLogico = serialLogico;

      //Act
      var validacion = _validator.Validate(_solicitudes.ConsultarSuscripcionDispositivoDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }
  }
}
