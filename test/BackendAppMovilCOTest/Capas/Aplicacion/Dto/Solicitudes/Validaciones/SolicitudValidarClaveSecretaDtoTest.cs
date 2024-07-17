using Aplicacion.Dto.Solicitudes.Validaciones;
using BackendAppMovilCOTest.Capas.Compartida;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudValidarClaveSecretaDtoTest
  {
    private SolicitudesCompartidasEmpresaAutenticacion _solicitudes = new();
    private SolicitudValidarClaveSecretaDtoValidate _solicitudValidarClaveSecretaDtoValidator;

    [SetUp]
    public void Setup()
    {
      _solicitudes = new();
      _solicitudValidarClaveSecretaDtoValidator = new();
    }

    [Test]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho255, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho256, false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validate_ClaveSecreta(string? claveSecreta, bool isValid)
    {
      //Arrange
      _solicitudes.DatosBearerTokenExisteValidarClaveSecretaDto.ClaveSecreta = claveSecreta;

      //Act
      var validate = _solicitudValidarClaveSecretaDtoValidator.Validate(_solicitudes.DatosBearerTokenExisteValidarClaveSecretaDto);

      //Assert
      Assert.That(validate.IsValid, Is.EqualTo(isValid));
    }

  }
}
