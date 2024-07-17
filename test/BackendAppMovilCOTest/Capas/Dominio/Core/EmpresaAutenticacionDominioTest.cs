using AutoMapper;
using BackendAppMovilCOTest.Capas.Compartida;
using Dominio.Core;
using Dominio.Entidad.Respuestas;
using Infraestructura.Interfaz;
using Transversal.Mapeo;

namespace BackendAppMovilCOTest.Capas.Dominio.Core
{
  public class EmpresaAutenticacionDominioTest
  {
    private Mock<IEmpresaAutenticacionRepositorio> _empresaAutenticacionRepositorio;
    private Mock<IRedisCacheRepositorio> _redisCacheRepositorio;
    private Mock<IEmpresaRepositorio> _empresaRepositorio;
    private EmpresaAutenticacionDominio _empresaAutenticacionDominio;
    private readonly RespuestasCompartidasEmpresa _respuestasCompartidasEmpresa = new();
    private SolicitudesCompartidasEmpresaAutenticacion _solicitudes;
    private RespuestasCompartidasEmpresaAutenticacion _respuestas;
    private readonly RespuestasCompartidas _respuestasCompartidas = new();

    [SetUp]
    public void Setup()
    {
      //Arrange general

      //Reutilizar el mapeo real existente en la capa Trasversal para no hacer Mock
      var perfilMapeo = new PerfilMapeo();
      var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(perfilMapeo));

      _solicitudes = new();
      _respuestas = new();

      _empresaAutenticacionRepositorio = new();
      _empresaAutenticacionRepositorio.Setup(c => c.validarClaveSecreta(It.Is<string>(cs => cs == ConstantesCompartidasFacturacion.ClaveSecretaValida), It.Is<string>(cs => cs == ConstantesCompartidasFacturacion.IdEmpresaExistente))).Returns(_respuestas.RespuestaDatosValidosValidarClaveSecreta);
      _empresaAutenticacionRepositorio.Setup(c => c.validarClaveSecreta(It.Is<string>(cs => cs != ConstantesCompartidasFacturacion.ClaveSecretaValida), It.Is<string>(cs => cs == ConstantesCompartidasFacturacion.IdEmpresaExistente))).Returns(_respuestas.RespuestaDatosInvalidosValidarClaveSecreta);
      _empresaAutenticacionRepositorio.Setup(c => c.validarClaveSecreta(It.Is<string>(cs => cs != ConstantesCompartidasFacturacion.ClaveSecretaValida), It.Is<string>(cs => cs != ConstantesCompartidasFacturacion.IdEmpresaExistente))).Returns(_respuestas.RespuestaDatosInvalidosValidarClaveSecreta);
      _empresaAutenticacionRepositorio.Setup(c => c.validarClaveSecreta(It.Is<string>(cs => cs == ConstantesCompartidasFacturacion.ClaveSecretaInactiva), It.Is<string>(cs => cs == ConstantesCompartidasFacturacion.IdEmpresaExistente))).Returns(_respuestas.RespuestaDatosInvalidosClaveSecretaInactiva);

      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.Is<string>(s => s == ConstantesCompartidasFacturacion.ValorBearerTokenValido))).Returns(_respuestas.EmpresaAutenticacionExitosa);
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.Is<string>(s => s != ConstantesCompartidasFacturacion.ValorBearerTokenValido))).Returns(_respuestas.EmpresaAutenticacionErronea);
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.Is<string>(s => s == ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid))).Returns(_respuestas.EmpresaAutenticacionExitosaBuscarDespuesDeTimeOutRedisCache);

      _redisCacheRepositorio = new();
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosa!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosaUsuarioNoEncontradoEnBD!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache && s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionTimeOutCache!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado && s.token != ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionUsuarioNoEncontradoEnCache!);


      _empresaRepositorio = new();
      _empresaRepositorio.Setup(r => r.ConsultarEmpresaPorId(It.Is<int>(c => c == ConstantesCompartidasFacturacion.IdEmpresaInvalidaInt))).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaDatosTokenInvalidos!);
      _empresaRepositorio.Setup(r => r.ConsultarEmpresaPorId(It.Is<int>(c => c == ConstantesCompartidasFacturacion.IdEmpresaExistenteInt))).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaExisteDB!);


      _empresaAutenticacionDominio = new(_empresaAutenticacionRepositorio.Object, _redisCacheRepositorio.Object, _empresaRepositorio.Object);
    }


    [Test]
    public void SeleccionarEstablecimiento_TimeoutRedisCache_DatosTokenvalidos_RespuestaExitosa()
    {
      //Arrange
      //Act
      var respuesta = _empresaAutenticacionDominio.ValidarClaveSecreta(_solicitudes.DatosBearerTokenExisteValidarClaveSecreta, ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache, ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoConsultaExitosaClaveSecreta));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoClaveSecretaEncontrada));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeClaveSecretaEncontrada));
        Assert.That(respuesta.IdEmpresa, Is.Not.Null);
      });
    }

    [Test]
    public void ValidarClaveSecreta_CuandoDatosExisten_DevuelveRespuesta200()
    {
      //Arrange
      //Act
      var respuesta = _empresaAutenticacionDominio.ValidarClaveSecreta(_solicitudes.DatosBearerTokenExisteValidarClaveSecreta, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoConsultaExitosaClaveSecreta));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoClaveSecretaEncontrada));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeClaveSecretaEncontrada));
        Assert.That(respuesta.IdEmpresa, Is.Not.Null);
      });
    }

    [Test]
    public void ValidarClaveSecreta_CuandoDatosNoExisten_DevuelveRespuesta401()
    {
      //Arrange
      _solicitudes.DatosBearerTokenExisteValidarClaveSecreta.ClaveSecreta = ConstantesCompartidasFacturacion.ClaveSecretaInvalida;
      //Act
      var respuesta = _empresaAutenticacionDominio.ValidarClaveSecreta(_solicitudes.DatosBearerTokenExisteValidarClaveSecreta, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoConsultaInvalidaClaveSecreta));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoClaveSecretaInvalida));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeClaveSecretaInvalida));
        Assert.That(respuesta.IdEmpresa, Is.Null);
      });
    }

    [Test]
    public void ValidarClaveSecreta_CuandoTokenEsInvalido_DevuelveRespuesta401()
    {
      //Arrange
      //Act
      var respuesta = _empresaAutenticacionDominio.ValidarClaveSecreta(_solicitudes.DatosBearerTokenExisteValidarClaveSecreta, ConstantesCompartidasFacturacion.BearerTokenInvalido, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoSessionCerrada));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoClaveSecretaInvalida));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeSesionCerrada));
        Assert.That(respuesta.IdEmpresa, Is.Null);
      });
    }

    [Test]
    public void ValidarClaveSecreta_CuandoClave_esInactiva_DevuelveRespuesta401()
    {
      //Arrange
      _solicitudes.DatosBearerTokenExisteValidarClaveSecreta.ClaveSecreta = ConstantesCompartidasFacturacion.ClaveSecretaInactiva;
      //Act
      var respuesta = _empresaAutenticacionDominio.ValidarClaveSecreta(_solicitudes.DatosBearerTokenExisteValidarClaveSecreta, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoConsultaInvalidaClaveSecreta));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoClaveSecretaInvalida));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeClaveSecretaInactiva));
        Assert.That(respuesta.IdEmpresa, Is.Null);
      });
    }

  }
}
