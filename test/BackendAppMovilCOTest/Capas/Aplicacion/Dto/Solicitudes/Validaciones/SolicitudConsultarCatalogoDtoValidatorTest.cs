using Aplicacion.Dto.Solicitudes.Validaciones;
using Aplicacion.Dto.Solicitudes;
using Microsoft.Extensions.Configuration;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudConsultarCatalogoDtoValidatorTest
  {
    private SolicitudConsultarCatalogoDto _solicitudConsultarCatalogoDto;
    private SolicitudConsultarCatalogoDtoValidator _solicitudConsultarCatalogoDtoValidator;

    [SetUp]
    public void SetUp()
    {
      Dictionary<string, string> appSettings = new()
      {
        { "BaseDeDatos:SoloLectura:Catalogos:DetallesTributarios", "BackendAppMovil.ConsultarCatalogoDetallesTributarios" },
        { "BaseDeDatos:SoloLectura:Catalogos:OrigenFacturacion", "BackendAppMovil.ConsultarCatalogoOrigenFacturacion" },
        { "BaseDeDatos:SoloLectura:Catalogos:Responsabilidades", "BackendAppMovil.ConsultarCatalogoResponsabilidades" },
        { "BaseDeDatos:SoloLectura:Catalogos:TipoDocumento", "BackendAppMovil.ConsultarCatalogoTipoDocumento" },
        { "BaseDeDatos:SoloLectura:Catalogos:TipoOperacion", "BackendAppMovil.ConsultarCatalogoTipoOperacion" }
      };
      IConfiguration configuracion = new ConfigurationBuilder().AddInMemoryCollection(appSettings!).Build();

      _solicitudConsultarCatalogoDto = new()
      {
        Identificador = null
      };
      _solicitudConsultarCatalogoDtoValidator = new(configuracion);
    }

    [Test]
    [TestCase(null, true)]
    [TestCase("DetallesTributarios", true)]
    [TestCase("OrigenFacturacion", true)]
    [TestCase("Responsabilidades", true)]
    [TestCase("TipoDocumento", true)]
    [TestCase("TipoOperacion", true)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("*", false)]
    [TestCase("a", false)]
    [TestCase("Z", false)]
    [TestCase("0", false)]
    [TestCase("1", false)]
    [TestCase("-1", false)]
    [TestCase("CatalogoNoExistente", false)]
    [TestCase("CatalogoNoImplementado", false)]
    public void Validar_Identificador(string? identificador, bool esValido)
    {
      //Arrange
      _solicitudConsultarCatalogoDto.Identificador = identificador;

      //Act
      var validacion = _solicitudConsultarCatalogoDtoValidator.Validate(_solicitudConsultarCatalogoDto);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }
  }
}
