using Infraestructura.Repositorio;
using Microsoft.Extensions.Configuration;

namespace BackendAppMovilCOTest.Capas.Infraestructura.Repositorio
{
  public class ReportesRepositorioTest
  {
    private IConfiguration _configuracion;

    private const string _urlReportesFacturacion = "http://devrestreporte.thefactoryhka.com.co";
    private const string _apiReporteEnLineaFacturacion = "api/Reportes/Portal/EnLinea";
    private const string _codigoReporteGeneralEnLineaFacturacion = "9";
    private const string _urlReportesRecepcion = "http://devreportesrecepcionrest.thefactoryhka.com.co";
    private const string _apiReporteEnLineaRecepcion = "api/Reportes/Portal/EnLinea";
    private const string _codigoReporteGeneralEnLineaRecepcion = "15";
    private const string _urlReportesNomina = "http://devreportesnominarest.thefactoryhka.com.co";
    private const string _apiReporteEnLineaNomina = "api/Reportes/Portal/EnLinea";
    private const string _codigoReporteGeneralEnLineaNomina = "11";

    [SetUp]
    public void SetUp()
    {
      //Arrange General
      Dictionary<string, string> appSettings = new()
      {
        { "ServiciosFacturacion:ReportesRest:Url", _urlReportesFacturacion },
        { "ServiciosFacturacion:ReportesRest:ReporteEnLinea:Api", _apiReporteEnLineaFacturacion },
        { "ServiciosFacturacion:ReportesRest:ReporteEnLinea:CodigoReporteGeneral", _codigoReporteGeneralEnLineaFacturacion },
        { "ServiciosRecepcion:ReportesRest:Url", _urlReportesRecepcion },
        { "ServiciosRecepcion:ReportesRest:ReporteEnLinea:Api", _apiReporteEnLineaRecepcion },
        { "ServiciosRecepcion:ReportesRest:ReporteEnLinea:CodigoReporteGeneral", _codigoReporteGeneralEnLineaRecepcion },
        { "ServiciosNomina:ReportesRest:Url", _urlReportesNomina },
        { "ServiciosNomina:ReportesRest:ReporteEnLinea:Api", _apiReporteEnLineaNomina },
        { "ServiciosNomina:ReportesRest:ReporteEnLinea:CodigoReporteGeneral", _codigoReporteGeneralEnLineaNomina }
      };
      _configuracion = new ConfigurationBuilder().AddInMemoryCollection(appSettings!).Build();
    }

    [Test]
    [TestCase("1", _urlReportesFacturacion)]
    [TestCase("2", _urlReportesRecepcion)]
    [TestCase("3", _urlReportesNomina)]
    [TestCase("", _urlReportesFacturacion)]
    [TestCase(" ", _urlReportesFacturacion)]
    [TestCase(null, _urlReportesFacturacion)]
    [TestCase("123", _urlReportesFacturacion)]
    [TestCase("uno", _urlReportesFacturacion)]
    [TestCase("dos", _urlReportesFacturacion)]
    [TestCase("***", _urlReportesFacturacion)]
    public void ObtenerUrlServicio(string? sistema, string respuestaEsperada)
    {
      //Arrange
      ReportesRepositorio reportesRepositorio = new ReportesRepositorio(_configuracion);

      //Act
      var respuesta = reportesRepositorio.ObtenerUrlServicio(sistema);

      //Assert
      Assert.That(respuesta, Is.EqualTo(respuestaEsperada));
    }

    [Test]
    [TestCase("1", _apiReporteEnLineaFacturacion)]
    [TestCase("2", _apiReporteEnLineaRecepcion)]
    [TestCase("3", _apiReporteEnLineaNomina)]
    [TestCase("", _apiReporteEnLineaFacturacion)]
    [TestCase(" ", _apiReporteEnLineaFacturacion)]
    [TestCase(null, _apiReporteEnLineaFacturacion)]
    [TestCase("123", _apiReporteEnLineaFacturacion)]
    [TestCase("uno", _apiReporteEnLineaFacturacion)]
    [TestCase("dos", _apiReporteEnLineaFacturacion)]
    [TestCase("***", _apiReporteEnLineaFacturacion)]
    public void ObtenerApiReporteEnLinea(string? sistema, string respuestaEsperada)
    {
      //Arrange
      ReportesRepositorio reportesRepositorio = new ReportesRepositorio(_configuracion);

      //Act
      var respuesta = reportesRepositorio.ObtenerApiReporteEnLinea(sistema);

      //Assert
      Assert.That(respuesta, Is.EqualTo(respuestaEsperada));
    }

    [Test]
    [TestCase("1", _codigoReporteGeneralEnLineaFacturacion)]
    [TestCase("2", _codigoReporteGeneralEnLineaRecepcion)]
    [TestCase("3", _codigoReporteGeneralEnLineaNomina)]
    [TestCase("", _codigoReporteGeneralEnLineaFacturacion)]
    [TestCase(" ", _codigoReporteGeneralEnLineaFacturacion)]
    [TestCase(null, _codigoReporteGeneralEnLineaFacturacion)]
    [TestCase("123", _codigoReporteGeneralEnLineaFacturacion)]
    [TestCase("uno", _codigoReporteGeneralEnLineaFacturacion)]
    [TestCase("dos", _codigoReporteGeneralEnLineaFacturacion)]
    [TestCase("***", _codigoReporteGeneralEnLineaFacturacion)]
    public void ObtenerCodigoReporteGeneralEnLinea(string? sistema, string respuestaEsperada)
    {
      //Arrange
      ReportesRepositorio reportesRepositorio = new ReportesRepositorio(_configuracion);

      //Act
      var respuesta = reportesRepositorio.ObtenerCodigoReporteGeneralEnLinea(sistema);

      //Assert
      Assert.That(respuesta, Is.EqualTo(respuestaEsperada));
    }
  }
}
