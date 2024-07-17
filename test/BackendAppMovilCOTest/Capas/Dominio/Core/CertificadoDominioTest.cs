using BackendAppMovilCOTest.Capas.Compartida;
using Dominio.Core;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;

namespace BackendAppMovilCOTest.Capas.Dominio.Core
{
  public class CertificadoDominioTest
  {
    private Mock<ICertificadoRepositorio> _certificadoRepositorio;
    private Mock<IEmpresaRepositorio> _empresaRepositorio;
    private Mock<IEmpresaAutenticacionRepositorio> _empresaAutenticacionRepositorio = new();
    private CertificadoDominio _certificadoDominio;
    private RespuestasCompartidasCertificado _respuestas;
    private readonly RespuestasCompartidasEmpresaAutenticacion _respuestasCompartidasEmpresaAutenticacion = new();
    private SolicitudesCompartidasCertificado _solicitudes;
    private readonly RespuestasCompartidasEmpresa _respuestasEmpresa = new();

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      _solicitudes = new();
      _respuestas = new();
      _respuestasEmpresa.InicializarRespuestas();

      _certificadoRepositorio = new();
      _certificadoRepositorio.Setup(c => c.Consultar(It.Is<SolicitudConsultarFacturacion>(s => s.Nit != ConstantesCompartidasFacturacion.NitInexistente))).Returns(_respuestas.DatosExistentesConsultarCertificado);
      _certificadoRepositorio.Setup(c => c.Consultar(It.Is<SolicitudConsultarFacturacion>(s => s.Nit == ConstantesCompartidasFacturacion.NitInexistente))).Returns(_respuestas.DatosNitNoExisteConsultarCertificado);
      _certificadoRepositorio.Setup(c => c.Consultar(It.Is<SolicitudConsultarFacturacion>(s => s.TokenClave == ConstantesCompartidasFacturacion.TokenClaveInvalido))).Returns(_respuestas.DatosTokensInvalidosConsultarCertificado);
      _certificadoRepositorio.Setup(c => c.Consultar(It.Is<SolicitudConsultarFacturacion>(s => s.TokenClave == null))).Returns(_respuestas.DatosInvalidosConsultarCertificado);

      _empresaRepositorio = new();
      _empresaRepositorio.Setup(e => e.ConsultarEmpresaPorId(It.IsAny<int>())).Returns(_respuestasEmpresa.RespuestaConsultarEmpresaExisteDB);
      _empresaRepositorio.Setup(e => e.ConsultarEmpresaPorId(It.Is<int>(id => id == Convert.ToInt32(ConstantesCompartidasFacturacion.IdEmpresaInexistente)))).Returns(_respuestasEmpresa.RespuestaConsultarEmpresaNoExisteDB);

      _empresaAutenticacionRepositorio = new();
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.IsAny<string>())).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosa);

      _certificadoDominio = new CertificadoDominio(_certificadoRepositorio.Object, _empresaRepositorio.Object);
    }

    #region Consultar Certificado
    [Test]
    public void ConsultarCertificado_CuandoNitNoExiste_DevuelveError404()
    {
      //Arrange

      //Act
      var respuesta = _certificadoDominio.Consultar(_solicitudes.DatosNitNoExisteConsultarCertificado);

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
      _certificadoRepositorio.Setup(c => c.Consultar(It.Is<SolicitudConsultarFacturacion>(s => s.TokenEmpresa == ConstantesCompartidasFacturacion.TokenEmpresaInvalido))).Returns(_respuestas.DatosTokensInvalidosConsultarCertificado);
      //Arrange
      _solicitudes.DatosExistentesConsultarCertificado.TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaInvalido;

      //Act
      var respuesta = _certificadoDominio.Consultar(_solicitudes.DatosExistentesConsultarCertificado);

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
