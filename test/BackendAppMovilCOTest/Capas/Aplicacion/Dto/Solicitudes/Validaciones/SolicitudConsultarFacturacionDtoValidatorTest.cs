using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudConsultarFacturacionDtoValidatorTest
  {
    private SolicitudConsultarFacturacionDto _solicitudConsultarFacturacionDto;
    private SolicitudConsultarFacturacionDtoValidator _solicitudConsultarFacturacionDtoValidator;

    [SetUp]
    public void Setup()
    {
      _solicitudConsultarFacturacionDto = new SolicitudConsultarFacturacionDto
      {
        IdEmpresa = "1",
        Nit = "123456789",
        TokenEmpresa = "a123a123a456a456a789a789asd",
        TokenClave = null,
        Plataforma = "TFHKA"
      };
      _solicitudConsultarFacturacionDtoValidator = new SolicitudConsultarFacturacionDtoValidator();
    }

    [Test]
    [TestCase("DIAN", true)]
    [TestCase("TFHKA", true)]
    [TestCase("dian", false)]
    [TestCase("tfhka", false)]
    [TestCase("Dian", false)]
    [TestCase("Tfhka", false)]
    [TestCase(null, true)]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEnteroLargo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConValorMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroLargoConAnchoMayorAlMaximo, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50, false)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51, false)]
    [TestCase("-1", false)]
    [TestCase("0", false)]
    [TestCase("1", false)]
    [TestCase("1.2", false)]
    [TestCase("2,3", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("*", false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_Plataforma(string? plataforma, bool esValido)
    {
      _solicitudConsultarFacturacionDto.Plataforma = plataforma;

      var validacion = _solicitudConsultarFacturacionDtoValidator.Validate(_solicitudConsultarFacturacionDto);

      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }
  }
}
