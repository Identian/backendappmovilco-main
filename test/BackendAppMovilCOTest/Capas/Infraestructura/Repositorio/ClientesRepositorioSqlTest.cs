using BackendAppMovilCOTest.Capas.Compartida;
using Infraestructura.Repositorio;
using Infraestructura.Repositorio.GestionDeAcceso;

namespace BackendAppMovilCOTest.Capas.Infraestructura.Repositorio
{
    public class ClientesRepositorioSqlTest
  {
    readonly RespuestasCompartidasClientesRepositorioSql _respuestasCompartidas = new();

    [SetUp]
    public void SetUp()
    {
      //
    }

    [Test]
    [TestCase("aramos@thefactoryhka.com", ";", "0")]
    [TestCase("arivero@thefactoryhka.com", ";", "1")]
    [TestCase("lucas.graterol.ve@gmail.com", ";", "0")]
    [TestCase("squad_quantico@thefactoryhka.com", ";", "1")]
    public void ObtenerDestinatario(string emails, string separadorEmail, string canalDeEntrega)
    {
      //Arrange
      var respuestaEsperada = RespuestasCompartidasClientesRepositorioSql.ListaDestinatariosClienteRegistrado(emails, canalDeEntrega);

      //Act
      var respuestaObtenida = ClientesRepositorioSql.ObtenerDestinatario(emails, separadorEmail, canalDeEntrega);

      //Assert
      respuestaObtenida.Should().BeEquivalentTo(respuestaEsperada);
    }

    [Test]
    [TestCase("adramos@thefactoryhka.com;arivero@thefactoryhka.com", ";", 2)]
    [TestCase("adramos@thefactoryhka.com,arivero@thefactoryhka.com,lgraterol@thefactoryhka.com", ",", 3)]
    [TestCase("squad_quantico@thefactoryhka.com;", ";", 1)]
    [TestCase(";squad_quantico@thefactoryhka.com;", ";", 1)]
    [TestCase("adramos@thefactoryhka.com,arivero@thefactoryhka.com,lgraterol@thefactoryhka.com", ";", 1)]
    [TestCase(";;;", ",", 1)]
    public void ObtenerDestinatario_MultiplesCorreos(string emails, string separadorEmail, int numeroCorreos)
    {
      //Arrange

      //Act
      var respuestaObtenida = ClientesRepositorioSql.ObtenerDestinatario(emails, separadorEmail, "0");

      //Assert
      Assert.That(respuestaObtenida!.FirstOrDefault()!.Email!.Count(), Is.EqualTo(numeroCorreos));
    }

    [Test]
    [TestCase("", ";")]
    [TestCase(";;", ";")]
    [TestCase(",,,", ",")]
    [TestCase(null, ";")]
    public void ObtenerDestinatario_DevuelveNull(string? emails, string separadorEmail)
    {
      //Arrange

      //Act
      var respuestaObtenida = ClientesRepositorioSql.ObtenerDestinatario(emails, separadorEmail, "0");

      //Assert
      Assert.That(respuestaObtenida, Is.Null);
    }


    [Test]
    public void ObtenerDireccionCliente()
    {
      //Act
      var respuestaObtenida = ClientesRepositorioSql.ObtenerDireccion(_respuestasCompartidas.DireccionesClienteRegistrado, "1");

      //Assert
      respuestaObtenida.Should().BeEquivalentTo(_respuestasCompartidas.ClienteRegistrado.DireccionCliente);
    }

    [Test]
    public void ObtenerDireccionFiscalCliente()
    {
      //Act
      var respuestaObtenida = ClientesRepositorioSql.ObtenerDireccion(_respuestasCompartidas.DireccionesClienteRegistrado, "2");

      //Assert
      respuestaObtenida.Should().BeEquivalentTo(_respuestasCompartidas.ClienteRegistrado.DireccionFiscal);
    }

    [Test]
    public void ObtenerInformacionLegal()
    {
      //Act
      var respuestaObtenida = ClientesRepositorioSql.ObtenerInformacionLegal(_respuestasCompartidas.ClienteRegistrado);

      //Assert
      respuestaObtenida.Should().BeEquivalentTo(_respuestasCompartidas.ClienteRegistrado.InformacionLegalCliente);
    }
  }
}
