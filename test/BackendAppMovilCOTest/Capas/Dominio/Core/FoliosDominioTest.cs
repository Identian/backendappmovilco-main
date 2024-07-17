using BackendAppMovilCOTest.Capas.Compartida;
using Dominio.Core;
using FluentAssertions;
using Infraestructura.Interfaz;

namespace BackendAppMovilCOTest.Capas.Dominio.Core
{
  public class FoliosDominioTest
  {
    private Mock<IFoliosRepositorio> _foliosRepositorio = new();
    private Mock<IEmpresaAutenticacionRepositorio> _empresaAutenticacionRepositorio = new();
    private RespuestasCompartidasFolios _respuestasCompartidasFolios;

    [SetUp]
    public void Setup()
    {
      _respuestasCompartidasFolios = new();
      _respuestasCompartidasFolios.InicializarRespuestas();

      _foliosRepositorio = new();
      _foliosRepositorio.Setup(d => d.ConsultarResumen(It.Is<string>(rc => rc == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(rc => rc == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidasFolios.RespuestaConsultarResumenFoliosExitosa);
      _foliosRepositorio.Setup(de => de.ConsultarResumen(It.Is<string>(rce => rce == ConstantesCompartidasFacturacion.IdEmpresaInexistente), It.Is<string>(rce => rce == ConstantesCompartidasFacturacion.BearerTokenInvalido))).Returns(_respuestasCompartidasFolios.RespuestaConsultarResumenFoliosNoEncontrado);

      _empresaAutenticacionRepositorio = new();
      _empresaAutenticacionRepositorio.Setup(d => d.ObtenerDatosToken(It.Is<string>(rc => rc == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidasFolios.RespuestaEmpresaAutenticacionExitosa);
      _empresaAutenticacionRepositorio.Setup(de => de.ObtenerDatosToken(It.Is<string>(rce => rce == ConstantesCompartidasFacturacion.BearerTokenInvalido))).Returns(_respuestasCompartidasFolios.RespuestaEmpresaAutenticacionNoEncontrado);
                 
    }
    
    [Test]
    [TestCase(ConstantesCompartidasFacturacion.BearerTokenValido)]
    public void ConsultarResumen_Exitoso(string bearerToken)
    {
      Mock<IAutenticacionFoliosRepositorio> autenticacionFoliosRepositorio = new();    
      autenticacionFoliosRepositorio.Setup(d => d.AutenticarUsuarioFolios()).Returns(_respuestasCompartidasFolios.RespuestaAutenticacionFoliosExitosa);     

      FoliosDominio foliosDominio = new(_foliosRepositorio.Object, _empresaAutenticacionRepositorio.Object,autenticacionFoliosRepositorio.Object);
      
      var respuestaExitosa = foliosDominio.ConsultarResumen(bearerToken);
         
      respuestaExitosa.Should().BeEquivalentTo(_respuestasCompartidasFolios.RespuestaConsultarResumenFoliosExitosa);
    } 
    
    [Test]
    [TestCase(ConstantesCompartidasFacturacion.BearerTokenInvalido)]
    public void ConsultarResumen_Erroneo(string bearerToken)
    {
      Mock<IAutenticacionFoliosRepositorio> autenticacionFoliosRepositorio = new();
      autenticacionFoliosRepositorio.Setup(de => de.AutenticarUsuarioFolios()).Returns(_respuestasCompartidasFolios.RespuestaAutenticacionFoliosNoEncontrado);
      FoliosDominio foliosDominio= new(_foliosRepositorio.Object, _empresaAutenticacionRepositorio.Object,autenticacionFoliosRepositorio.Object);
      
      var respuestaErronea = foliosDominio.ConsultarResumen(bearerToken);
         
      respuestaErronea.Should().BeEquivalentTo(_respuestasCompartidasFolios.RespuestaConsultarResumenFoliosNoEncontrado);
    }
  }
}
