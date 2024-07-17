using Aplicacion.Principal;
using AutoMapper;
using BackendAppMovilCOTest.Capas.Compartida;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Transversal.Mapeo;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Principal
{
  public class CertificadoAplicacionTest
  {
    private Mock<ICertificadoDominio> _certificadoDominio = new();
    private CertificadoAplicacion _certificadoAplicacion;
    private RespuestasCompartidasCertificado _respuestas = new();
    private SolicitudesCompartidasCertificado _solicitudes = new();
    private IMapper _mapeador;

    [SetUp]
    public void Setup()
    {
      //Arrange general

      //Reutilizar el mapeo real existente en la capa Trasversal para no hacer Mock
      var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PerfilMapeo()));
      _mapeador = new Mapper(mapperConfiguration);

      _solicitudes = new();
      _respuestas = new();

      _certificadoDominio = new();
      _certificadoDominio.Setup(c => c.Consultar(It.Is<SolicitudConsultarFacturacion>(s => s.Nit != ConstantesCompartidasFacturacion.NitInexistente))).Returns(_respuestas.DatosExistentesConsultarCertificado);
      _certificadoDominio.Setup(c => c.Consultar(It.Is<SolicitudConsultarFacturacion>(s => s.Nit == ConstantesCompartidasFacturacion.NitInexistente))).Returns(_respuestas.DatosNitNoExisteConsultarCertificado);
      _certificadoDominio.Setup(c => c.Consultar(It.Is<SolicitudConsultarFacturacion>(s => s.TokenEmpresa == ConstantesCompartidasFacturacion.TokenEmpresaInvalido))).Returns(_respuestas.DatosTokensInvalidosConsultarCertificado);
      _certificadoDominio.Setup(c => c.Consultar(It.Is<SolicitudConsultarFacturacion>(s => s.TokenClave == ConstantesCompartidasFacturacion.TokenClaveInvalido))).Returns(_respuestas.DatosTokensInvalidosConsultarCertificado);

      _certificadoAplicacion = new(_certificadoDominio.Object, _mapeador);
    }

    #region Consultar Certificado
    [Test]
    public void ConsultarCertificado_CuandoDatosExisten_DevuelveCertificado()
    {
      //Arrange

      //Act
      var respuesta = _certificadoAplicacion.Consultar(_solicitudes.DatosExistentesConsultarCertificadoDto);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosValidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosValidos));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeDatosValidos));
        Assert.That(respuesta.Certificado, Is.Not.Null);
      });
    }

    [Test]
    public void ConsultarCertificado_CuandoNitNoExiste_DevuelveError404()
    {
      //Arrange

      //Act
      var respuesta = _certificadoAplicacion.Consultar(_solicitudes.DatosNitNoExisteConsultarCertificadoDto);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoNitNoExiste));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoNitNoExiste));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeNitNoExiste));
        Assert.That(respuesta.Certificado, Is.Null);
      });
    }

    [Test]
    public void ConsultarCertificado_CuandoTokenEmpresaEsInValido_DevuelveError401()
    {
      //Arrange
      _solicitudes.DatosExistentesConsultarCertificadoDto.TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaInvalido;

      //Act
      var respuesta = _certificadoAplicacion.Consultar(_solicitudes.DatosExistentesConsultarCertificadoDto);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoTokensInvalidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoTokensInvalidos));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeTokensInvalidos));
        Assert.That(respuesta.Certificado, Is.Null);
      });
    }

    [Test]
    public void ConsultarCertificado_CuandoTokenClaveEsInValido_DevuelveError401()
    {
      //Arrange
      _solicitudes.DatosExistentesConsultarCertificadoDto.TokenClave = ConstantesCompartidasFacturacion.TokenClaveInvalido;

      //Act
      var respuesta = _certificadoAplicacion.Consultar(_solicitudes.DatosExistentesConsultarCertificadoDto);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoTokensInvalidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoTokensInvalidos));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeTokensInvalidos));
        Assert.That(respuesta.Certificado, Is.Null);
      });
    }
    #endregion
  }
}