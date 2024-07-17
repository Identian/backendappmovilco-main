using Infraestructura.Repositorio;

namespace BackendAppMovilCOTest.Capas.Infraestructura.Repositorio
{
  public class CifradoRepositorioTest
  {
    [SetUp]
    public void Setup()
    {
      //Arrange general
    }

    [Test]
    [TestCase("abcdefghi", "5F86135EF23F7F8C854F91EFB635C1637AE66AB948D1CAB9D96185DF6F9C4617EAE8B3429EEFF6D90957F0F2BAFDC161F428ABBE9DFA6A2BF83BA127152E10F8")]
    [TestCase("123456789", "C99A018DBCCEB3ECA8A40C069B07AAD2622F2A5D08AE40DCBC288AD87316C61789F6E9C3837430F48C67E26B4BD3C24B3664C745E748CEDFC11F52E68317411A")]
    [TestCase("abcde12345", "D350D90FEFACCE2076B8F712B18B15DE4B1B4D3F8922D6D22012D5FA477E5EDC95723FE994E142EB4A1CA4A94BA8CC4226E813B5F2208A1456F0F621C3B76156")]
    [TestCase(null, "")]
    [TestCase("", "")]
    public void CifrarTexto(string? texto, string textoCifrado)
    {
      //Arrange
      CifradoRepositorio cifradoRepositorio = new();

      //Act
      string respuesta = cifradoRepositorio.CifrarTexto(texto);

      //Assert
      Assert.That(respuesta, Is.EqualTo(textoCifrado));
    }
  }
}
