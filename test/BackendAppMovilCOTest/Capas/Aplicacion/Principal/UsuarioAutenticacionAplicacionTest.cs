using Aplicacion.Dto.Solicitudes;
using Aplicacion.Principal;
using AutoMapper;
using BackendAppMovilCOTest.Capas.Compartida;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Transversal.Mapeo;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Principal
{
  public class UsuarioAutenticacionAplicacionTest
  {
    private Mock<IUsuarioAutenticacionDominio> _usuarioAutenticacionDominio = new();
    private Mapper _mapeador;
    private readonly RespuestasCompartidas _respuestasCompartidas = new();
    private const string _usuarioInvalido = "invalido@email.com";
    private const string _usuarioInactivo = "inactivo@email.com";
    private const string _contrasenaInvalida = "123456789";
    private const string _empresaInactiva = "empresaInvalida@email.com";
    private const string _empresaUsuarioInactivo = "empresaUsuarioInvalido@email.com";
    private const string _empresaTokensInvalidos = "empresaTokensInvalidos@email.com";

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new PerfilMapeo()));
      _mapeador = new Mapper(mappingConfig);
      _usuarioAutenticacionDominio = new();
      _usuarioAutenticacionDominio.Setup(u => u.AutenticarUsuario(It.IsAny<UsuarioAutenticacion>())).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosa!);
      _usuarioAutenticacionDominio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _usuarioInvalido)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionUsuarioInvalido!);
      _usuarioAutenticacionDominio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _usuarioInactivo)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionUsuarioInactivo!);
      _usuarioAutenticacionDominio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Contrasena, _contrasenaInvalida)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionContrasenaInvalida!);
      _usuarioAutenticacionDominio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _empresaInactiva)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionEmpresaInactiva!);
      _usuarioAutenticacionDominio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _empresaUsuarioInactivo)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionEmpresaUsuarioInactivo!);
      _usuarioAutenticacionDominio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _empresaTokensInvalidos)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionTokensInvalidos!);
      _usuarioAutenticacionDominio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar)))).Returns(_respuestasCompartidas.RespuestaUsuarioRolEstandarAutenticacionExitosa!);
    }

    [Test]
    [TestCase("empleado@email.com", "123456789ASDFG", RespuestasCompartidas.responseAutenticacionExitosa, RespuestasCompartidas.messageAutenticacionExitosa, true)]
    [TestCase("empleado@empresa.com.co", "123456789!#$%", RespuestasCompartidas.responseAutenticacionExitosa, RespuestasCompartidas.messageAutenticacionExitosa, true)]
    [TestCase("empleado_1@empresa.com.co", "123456789!#$%", RespuestasCompartidas.responseAutenticacionExitosa, RespuestasCompartidas.messageAutenticacionExitosa, true)]
    [TestCase(_usuarioInvalido, "123456789ASDFG", RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionUsuarioInvalido, false)]
    [TestCase(_usuarioInactivo, "123456789ASDFG", RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionUsuarioInactivo, false)]
    [TestCase("empleado@email.com", _contrasenaInvalida, RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionContrasenaInvalida, false)]
    [TestCase(_empresaInactiva, "123456789ASDFG", RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionEmpresaInactiva, false)]
    [TestCase(_empresaUsuarioInactivo, "123456789ASDFG", RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionEmpresaUsuarioInactivo, false)]
    [TestCase(_empresaTokensInvalidos, "123456789ASDFG", RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionTokensInvalidos, false)]
    [TestCase("empleado@email.com", "123456", RespuestasCompartidas.responseAutenticacionError, "Longitud incorrecta para 'Contrasena'", false)]
    [TestCase("empleado@email", "123456789ASDFG", RespuestasCompartidas.responseAutenticacionError, "Formato incorrecto para 'Usuario'", false)]
    [TestCase(null, "123456789ASDFG", RespuestasCompartidas.responseAutenticacionError, "'Usuario' es requerido", false)]
    [TestCase("", "123456789ASDFG", RespuestasCompartidas.responseAutenticacionError, "'Usuario' no puede estar vacío", false)]
    [TestCase("empleado@email.com", null, RespuestasCompartidas.responseAutenticacionError, "'Contrasena' es requerido", false)]
    [TestCase("empleado@email.com", "", RespuestasCompartidas.responseAutenticacionError, "'Contrasena' no puede estar vacío", false)]
    public void AutenticarUsuario(string? usuario, string? contrasena, string? responseEsperado, string? messageEsperado, bool retornaToken)
    {
      //Arrange
      UsuarioAutenticacionAplicacion usuarioAutenticacionAplicacion = new(_usuarioAutenticacionDominio.Object, _mapeador);
      UsuarioAutenticacionDto usuarioAutenticacionDto = new()
      {
        Usuario = usuario,
        Contrasena = contrasena
      };

      //Act
      var respuesta = usuarioAutenticacionAplicacion.AutenticarUsuario(usuarioAutenticacionDto);

      //Assert
      Assert.That(respuesta.response, Is.EqualTo(responseEsperado));
      Assert.That(respuesta.message, Is.EqualTo(messageEsperado));
      if (retornaToken)
      {
        Assert.That(!string.IsNullOrEmpty(respuesta.token), Is.True);
      }
    }

    [Test]
    public void CuandoRolUsuarioEsEstandar_AutenticarUsuario_DevuelveAutenticacionExitosaSinClaveSecretaContribuyente()
    {
      //Arrange
      UsuarioAutenticacionAplicacion usuarioAutenticacionAplicacion = new(_usuarioAutenticacionDominio.Object, _mapeador);
      UsuarioAutenticacionDto usuarioAutenticacionDto = new()
      {
        Usuario = ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar,
        Contrasena = "Contraseña"
      };

      //Act
      var respuesta = usuarioAutenticacionAplicacion.AutenticarUsuario(usuarioAutenticacionDto);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaUsuarioRolEstandarAutenticacionExitosaDto);
    }
  }
}
