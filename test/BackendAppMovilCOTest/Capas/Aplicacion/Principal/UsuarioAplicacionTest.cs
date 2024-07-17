using Aplicacion.Principal;
using AutoMapper;
using BackendAppMovilCOTest.Capas.Compartida;
using Dominio.Interfaz;
using Transversal.Mapeo;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Principal
{
  public class UsuarioAplicacionTest
  {
    private Mock<IUsuarioDominio> _usuarioDominio = new();
    private readonly RespuestasCompartidasUsuarioFacturacion _respuestasCompartidasUsuarioFacturacion = new();
    private Mapper _mapeador;

    [SetUp]
    public void SetUp()
    {
      //Arrange General
      var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new PerfilMapeo()));
      _mapeador = new Mapper(mappingConfig);

      _usuarioDominio = new();
      _usuarioDominio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.BearerTokenValido), It.IsAny<string>())).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarUsuario);
      _usuarioDominio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.BearerTokenInvalido), It.IsAny<string>())).Returns(_respuestasCompartidasUsuarioFacturacion.DatosNoEncontradosConsultarUsuario);
      _usuarioDominio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t != ConstantesCompartidasFacturacion.BearerTokenValido && t != ConstantesCompartidasFacturacion.BearerTokenInvalido), It.IsAny<string>())).Returns(_respuestasCompartidasUsuarioFacturacion.SeHaCerradoSesionConsultarUsuario);
    }

    [Test]
    public void CuandoSesionEstaActiva_ConsultarInformacion_Devuelve200()
    {
      //Arrange
      UsuarioAplicacion usuarioAplicacion = new(_usuarioDominio.Object, _mapeador);

      //Act
      var respuesta = usuarioAplicacion.ConsultarInformacion(ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarUsuarioDto);
    }

    [Test]
    public void CuandoNoHayInformacionDeUsuario_ConsultarInformacion_Devuelve404()
    {
      //Arrange
      UsuarioAplicacion usuarioAplicacion = new(_usuarioDominio.Object, _mapeador);

      //Act
      var respuesta = usuarioAplicacion.ConsultarInformacion(ConstantesCompartidasFacturacion.BearerTokenInvalido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasUsuarioFacturacion.DatosNoEncontradosConsultarUsuarioDto);
    }

    [TestCase("132156")]
    [TestCase("ñalsls")]
    [TestCase("!#=)#$(%")]
    [TestCase("")]
    [TestCase(null)]
    public void CuandoSeHaCerradoSesion_ConsultarInformacion_Devuelve401(string? bearerToken)
    {
      //Arrange
      UsuarioAplicacion usuarioAplicacion = new(_usuarioDominio.Object, _mapeador);

      //Act
      var respuesta = usuarioAplicacion.ConsultarInformacion(bearerToken, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasUsuarioFacturacion.SeHaCerradoSesionConsultarUsuarioDto);
    }
  }
}
