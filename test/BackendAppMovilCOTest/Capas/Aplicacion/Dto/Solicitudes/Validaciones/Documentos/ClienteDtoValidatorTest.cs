using Aplicacion.Dto.Solicitudes.Validaciones.Documentos;
using BackendAppMovilCOTest.Capas.Compartida;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Dto.Solicitudes.Validaciones.Documentos
{
  public class ClienteDtoValidatorTest
  {
    private SolicitudesCompartidasEmitirDocumento _solicitudesCompartidasEmitirDocumento;
    private ClienteDtoValidator _clienteDtoValidator;

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      _solicitudesCompartidasEmitirDocumento = new();
      _clienteDtoValidator = new();
    }

    [Test]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_NombreRazonSocial(string? nombreRazonSocial, bool esValido)
    {
      //Arrange
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!.IdCliente = "00";
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!.NombreRazonSocial = nombreRazonSocial;

      //Act
      var validacion = _clienteDtoValidator.Validate(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }


    [Test]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_Notificar(string? notificar, bool esValido)
    {
      //Arrange
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!.IdCliente = "00";
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!.Notificar = notificar;

      //Act
      var validacion = _clienteDtoValidator.Validate(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }


    [Test]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validar_NumeroDocumento(string? numeroDocumento, bool esValido)
    {
      //Arrange
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!.IdCliente = "00";
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!.NumeroDocumento = numeroDocumento;

      //Act
      var validacion = _clienteDtoValidator.Validate(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }

    [Test]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validate_IdTipoIdentificacion(string? idTipoIdentificacion, bool esValido)
    {
      //Arrange
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!.IdCliente = "00";
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!.NumeroDocumento = idTipoIdentificacion;

      //Act
      var validacion = _clienteDtoValidator.Validate(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }


    [Test]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void Validate_TipoPersona(string? tipoPersona, bool esValido)
    {
      //Arrange
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!.IdCliente = "00";
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!.NumeroDocumento = tipoPersona;

      //Act
      var validacion = _clienteDtoValidator.Validate(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!);

      //Assert
      Assert.That(validacion.IsValid, Is.EqualTo(esValido));
    }
  }
}
