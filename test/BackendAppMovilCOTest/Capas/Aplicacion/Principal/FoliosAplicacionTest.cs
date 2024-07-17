using Aplicacion.Principal;
using AutoMapper;
using BackendAppMovilCOTest.Capas.Compartida;
using Dominio.Interfaz;
using Transversal.Mapeo;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Principal
{
  public class FoliosAplicacionTest
  {
    private IMapper _mapeador;
    private Mock<IFoliosDominio> _foliosDominio;
    private RespuestasCompartidasFolios _respuestasCompartidasFolios;

    [SetUp]
    public void Setup()
    {
      var perfilMapeo = new PerfilMapeo();
      var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(perfilMapeo));
      _mapeador = new Mapper(mapperConfiguration);

      _respuestasCompartidasFolios = new();
      _respuestasCompartidasFolios.InicializarRespuestas();

      _foliosDominio = new();
      _foliosDominio.Setup(d => d.ConsultarResumen(It.Is<string>(rc => rc == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidasFolios.RespuestaConsultarResumenFoliosExitosa);
      _foliosDominio.Setup(d => d.ConsultarResumen(It.Is<string>(rc => rc == ConstantesCompartidasFacturacion.BearerTokenInvalido))).Returns(_respuestasCompartidasFolios.RespuestaConsultarResumenFoliosNoEncontrado);

    }
    [Test]
    [TestCase(ConstantesCompartidasFacturacion.BearerTokenValido)]
    public void ConsultarResumen_Exitoso(string bearerToken)
    {
      FoliosAplicacion foliosAplicacion = new(_foliosDominio.Object, _mapeador);

      var respuesta = foliosAplicacion.ConsultarResumen(bearerToken);

      respuesta.Should().BeEquivalentTo(_respuestasCompartidasFolios.RespuestaConsultarResumenFoliosExitosa);
    }

    [Test]
    [TestCase(ConstantesCompartidasFacturacion.BearerTokenInvalido)]
    public void ConsultarResumen_Erroneo(string bearerToken)
    {
      FoliosAplicacion foliosAplicacion = new(_foliosDominio.Object, _mapeador);

      var respuesta = foliosAplicacion.ConsultarResumen(bearerToken);

      respuesta.Should().BeEquivalentTo(_respuestasCompartidasFolios.RespuestaConsultarResumenFoliosNoEncontrado);

    }
  }
}
