using Aplicacion.Principal;
using AutoMapper;
using BackendAppMovilCOTest.Capas.Compartida;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Transversal.Mapeo;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Principal
{
  public class EmpresaAutenticacionAplicacionTest
  {
    private Mock<IEmpresaAutenticacionDominio> _empresaAutenticacionDominio = new();
    private EmpresaAutenticacionAplicacion _empresaAutenticacionAplicacion;
    private RespuestasCompartidasEmpresaAutenticacion _respuestas = new();
    private SolicitudesCompartidasEmpresaAutenticacion _solicitudes = new();
    private IMapper _mapeador;

    [SetUp]
    public void SetUp()
    {
      //Arrange general

      //Reutilizar el mapeo real existente en la capa Trasversal para no hacer Mock
      var perfilMapeo = new PerfilMapeo();
      var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(perfilMapeo));
      _mapeador = new Mapper(mapperConfiguration);

      _solicitudes = new();
      _respuestas = new();

      _empresaAutenticacionDominio = new();
      _empresaAutenticacionDominio.Setup(c => c.ValidarClaveSecreta(It.Is<SolicitudValidarClaveSecreta>(s => s.ClaveSecreta == ConstantesCompartidasFacturacion.ClaveSecretaValida), It.Is<string>(t => t == ConstantesCompartidasFacturacion.BearerTokenValido), It.Is<string>(t => t == ConstantesCompartidasFacturacion.ValorBearerTokenValido))).Returns(_respuestas.RespuestaDatosValidosValidarClaveSecreta);
      _empresaAutenticacionDominio.Setup(c => c.ValidarClaveSecreta(It.Is<SolicitudValidarClaveSecreta>(s => s.ClaveSecreta != ConstantesCompartidasFacturacion.ClaveSecretaValida), It.Is<string>(t => t == ConstantesCompartidasFacturacion.BearerTokenValido), It.Is<string>(t => t == ConstantesCompartidasFacturacion.ValorBearerTokenValido))).Returns(_respuestas.RespuestaDatosInvalidosValidarClaveSecreta);
      _empresaAutenticacionDominio.Setup(c => c.ValidarClaveSecreta(It.Is<SolicitudValidarClaveSecreta>(s => s.ClaveSecreta == ConstantesCompartidasFacturacion.ClaveSecretaValida), It.Is<string>(t => t == ConstantesCompartidasFacturacion.BearerTokenInvalido), It.Is<string>(t => t == ConstantesCompartidasFacturacion.ValorBearerTokenValido))).Returns(_respuestas.SeHaCerradoSesionConsultarUsuario);
      _empresaAutenticacionDominio.Setup(c => c.ValidarClaveSecreta(It.Is<SolicitudValidarClaveSecreta>(s => s.ClaveSecreta == ConstantesCompartidasFacturacion.ClaveSecretaInactiva), It.Is<string>(t => t == ConstantesCompartidasFacturacion.BearerTokenValido), It.Is<string>(t => t == ConstantesCompartidasFacturacion.ValorBearerTokenValido))).Returns(_respuestas.RespuestaDatosInvalidosClaveSecretaInactiva);

      _empresaAutenticacionAplicacion = new(_empresaAutenticacionDominio.Object, _mapeador);
    }

    [Test]
    public void ValidarClaveSecreta_CuandoDatosExisten_DevuelveRespuesta200()
    {
      //Arrange

      //Act
      var respuesta = _empresaAutenticacionAplicacion.validarClaveSecreta(_solicitudes.DatosBearerTokenExisteValidarClaveSecretaDto, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

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
      _solicitudes.DatosBearerTokenExisteValidarClaveSecretaDto.ClaveSecreta = ConstantesCompartidasFacturacion.ClaveSecretaInvalida;
      //Act
      var respuesta = _empresaAutenticacionAplicacion.validarClaveSecreta(_solicitudes.DatosBearerTokenExisteValidarClaveSecretaDto, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoConsultaInvalidaClaveSecreta));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoClaveSecretaInvalida));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeClaveSecretaInvalida));
        Assert.That(respuesta.IdEmpresa, Is.Null);
      });
    }

    public void ValidarClaveSecreta_CuandoTokenEsInvalido_DevuelveRespuesta401()
    {
      //Arrange
      //Act
      var respuesta = _empresaAutenticacionAplicacion.validarClaveSecreta(_solicitudes.DatosBearerTokenExisteValidarClaveSecretaDto, ConstantesCompartidasFacturacion.BearerTokenInvalido, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

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
      _solicitudes.DatosBearerTokenExisteValidarClaveSecretaDto.ClaveSecreta = ConstantesCompartidasFacturacion.ClaveSecretaInactiva;
      //Act
      var respuesta = _empresaAutenticacionAplicacion.validarClaveSecreta(_solicitudes.DatosBearerTokenExisteValidarClaveSecretaDto, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

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
