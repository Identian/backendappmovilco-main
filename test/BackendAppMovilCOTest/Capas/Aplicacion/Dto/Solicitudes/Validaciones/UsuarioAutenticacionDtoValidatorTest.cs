using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones
{
  public class UsuarioAutenticacionDtoValidatorTest
  {
    private UsuarioAutenticacionDto _usuarioAutenticacionDto;
    private UsuarioAutenticacionDtoValidator _usuarioAutenticacionDtoValidator;

    [SetUp]
    public void Setup()
    {
      //Arrange general
      _usuarioAutenticacionDto = new UsuarioAutenticacionDto
      {
        Usuario = "usuario@email.com",
        Contrasena = "contrasena",
        TipoSesion = "Iniciar"
      };
      _usuarioAutenticacionDtoValidator = new UsuarioAutenticacionDtoValidator();
    }

    [Test]
    [TestCase("usuario@email.com", true)]
    [TestCase("12345679@email.com", true)]
    [TestCase("usuario_12346@email.com", true)]
    [TestCase("usuario_12346@empresa.com.co", true)]
    [TestCase(UtilidadesCadenas.Pruebas.CorreoAlfabetoMinusculas, true)]
    [TestCase(UtilidadesCadenas.Pruebas.CorreoAlfabetoMayusculas, true)]
    [TestCase("usuario@email.", false)]
    [TestCase("usuario@email", false)]
    [TestCase("usuario@", false)]
    [TestCase("@email.com", false)]
    [TestCase("32164", false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.CorreoConAnchoMayorAlMaximo, false)]
    public void Validar_Usuario(string? usuario, bool esValido)
    {
      //Arrange
      _usuarioAutenticacionDto.Usuario = usuario;

      //Act
      var validacion = _usuarioAutenticacionDtoValidator.Validate(_usuarioAutenticacionDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase("contrasena", true)]
    [TestCase("password123", true)]
    [TestCase("password123#$", true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, true)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, true)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, true)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, true)]
    [TestCase("abcdefg", false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    public void Validar_Contrasena(string? contrasena, bool esValido)
    {
      //Arrange
      _usuarioAutenticacionDto.Contrasena = contrasena;

      //Act
      var validacion = _usuarioAutenticacionDtoValidator.Validate(_usuarioAutenticacionDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("Iniciar", true)]
    [TestCase("Actualizar", true)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("Refrescar", false)]
    public void Validar_TipoSesion(string? tipoSesion, bool esValido)
    {
      //Arrange
      _usuarioAutenticacionDto.TipoSesion = tipoSesion;

      //Act
      var validacion = _usuarioAutenticacionDtoValidator.Validate(_usuarioAutenticacionDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase("1", true)]
    [TestCase("2", true)]
    [TestCase("0", false)]
    [TestCase("3", false)]
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
    public void Validar_TipoApp(string? TipoApp, bool esValido)
    {
      //Arrange
      _usuarioAutenticacionDto.TipoApp = TipoApp;

      //Act
      var validacion = _usuarioAutenticacionDtoValidator.Validate(_usuarioAutenticacionDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

  }
}
