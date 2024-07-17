using BackendAppMovilCOTest.Capas.Compartida;
using Dominio.Core;
using Dominio.Entidad.Respuestas;
using Infraestructura.Interfaz;

namespace BackendAppMovilCOTest.Capas.Dominio.Core
{
  public class UsuarioDominioTest
  {
    private Mock<IUsuarioRepositorio> _usuarioRepositorio = new();
    private Mock<IRedisCacheRepositorio> _redisCacheRepositorio = new();
    private Mock<IEmpresaAutenticacionRepositorio> _empresaAutenticacionRepositorio = new();
    private Mock<IEmpresaRepositorio> _empresaRepositorio = new();
    private readonly RespuestasCompartidas _respuestasCompartidas = new();
    private readonly RespuestasCompartidasUsuarioFacturacion _respuestasCompartidasUsuarioFacturacion = new();
    private readonly RespuestasCompartidasEmpresaAutenticacion _respuestasCompartidasEmpresaAutenticacion = new();

    [SetUp]
    public void SetUp()
    {
      //Arrange General
      _usuarioRepositorio = new();
      _usuarioRepositorio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.NitExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.CorreoUsuarioExistente))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarUsuario);
      _usuarioRepositorio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.NitExistente), It.Is<string>(t => t != ConstantesCompartidasFacturacion.CorreoUsuarioExistente))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosNoEncontradosConsultarUsuario);

      _usuarioRepositorio.Setup(u => u.ConsultarRollUsuario(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdUsuario))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarRollUsuario);

      _empresaAutenticacionRepositorio = new();
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.IsAny<string>())).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosa);

      _redisCacheRepositorio = new();
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosa);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosaUsuarioNoEncontradoEnBD);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionUsuarioNoEncontradoEnCache);
    }

    [Test]
    public void CuandoSesionEstaActiva_ConsultarInformacion_Devuelve200()
    {
      //Arrange
      UsuarioDominio usuarioDominio = new(_redisCacheRepositorio.Object, _usuarioRepositorio.Object, _empresaAutenticacionRepositorio.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = usuarioDominio.ConsultarInformacion(ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);


      //Asssert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarUsuario);
    }

    [Test]
    public void CuandoNoHayInformacionDeUsuario_ConsultarInformacion_Devuelve404()
    {
      //Arrange
      UsuarioDominio usuarioDominio = new(_redisCacheRepositorio.Object, _usuarioRepositorio.Object, _empresaAutenticacionRepositorio.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = usuarioDominio.ConsultarInformacion(ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado, ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasUsuarioFacturacion.DatosNoEncontradosConsultarUsuario);
    }

    [Test]
    [TestCase("132156")]
    [TestCase("ñalsls")]
    [TestCase("!#=)#$(%")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJleHAiOjE2ODY4NDkyNzgsImlhdCI6MTY4Njg0NTY3OCwiaXNzIjoiOTAwMzkwMTI2IiwiY29udGV4dCI6eyJ1c2VyIjp7ImVudGVycHJpc2VUb2tlbiI6ImQ0ODg3MTczODQ3NmY5MDU3NTQ4OGFkYzg0OGQ0MDJlODRlNmZkNTUiLCJlbnRlcHJpc2VJZCI6MTAwLCJlbnRlcnByaXNlTml0IjoiOTAwMzkwMTI2IiwiZW50ZXJwcmlzZXNjaGVtZWlkIjoiMzEiLCJlbnZpcm9tZW50IjowfX19.h47wPelIWhowjx0oI7Li0_MODKGPJ59Vrf6gA81062c")]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void CuandoSeHaCerradoSesion_ConsultarInformacion_Devuelve401(string? bearerToken)
    {
      //Arrange
      UsuarioDominio usuarioDominio = new(_redisCacheRepositorio.Object, _usuarioRepositorio.Object, _empresaAutenticacionRepositorio.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = usuarioDominio.ConsultarInformacion(bearerToken, bearerToken);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasUsuarioFacturacion.SeHaCerradoSesionConsultarUsuario);
    }
  }
}
