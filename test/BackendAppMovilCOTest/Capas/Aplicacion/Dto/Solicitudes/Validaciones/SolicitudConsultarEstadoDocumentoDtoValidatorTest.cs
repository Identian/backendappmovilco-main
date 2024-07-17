namespace Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudConsultarFacturacionDtoValidatorTest
  {
    private SolicitudConsultarEstadoDocumentoFacturacionDto _solicitudConsultarEstadoDocumentoDto;
    private SolicitudConsultarEstadoDocumentoDtoValidator _solicitudConsultarEstadoDocumentoDtoValidator;

    [SetUp]
    public void Setup()
    {
      _solicitudConsultarEstadoDocumentoDto = new SolicitudConsultarEstadoDocumentoFacturacionDto
      {
        IdEmpresa = "1",
        Nit = "123456789",
        TokenEmpresa = "a123a123a456a456a789a789asd",
        TokenClave = null,
        Plataforma = "TFHKA",
        Consecutivo = "ABC124",

      };
      _solicitudConsultarEstadoDocumentoDtoValidator = new SolicitudConsultarEstadoDocumentoDtoValidator();
    }



    [Test]
    [TestCase("TFHKA", true)]
    [TestCase("DIAN", true)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("OTRA", false)]
    public void Validar_Plataforma(string? plataforma, bool esValido)
    {
      _solicitudConsultarEstadoDocumentoDto.Plataforma = plataforma;

      var validacion = _solicitudConsultarEstadoDocumentoDtoValidator.Validate(_solicitudConsultarEstadoDocumentoDto);

      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }


    [Test]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("a", true)]
    [TestCase("AEFE154", true)]
    public void Validar_Consecutivo(string? consecutivo, bool esValido)
    {
      _solicitudConsultarEstadoDocumentoDto.Consecutivo = consecutivo;

      var validacion = _solicitudConsultarEstadoDocumentoDtoValidator.Validate(_solicitudConsultarEstadoDocumentoDto);

      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

  }
}
