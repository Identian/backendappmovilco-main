using Infraestructura.Interfaz;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Infraestructura.Repositorio
{
  public class CifradoRepositorio : ICifradoRepositorio
  {
    public string CifrarTexto(string texto)
    {
      string textoCifrado = "";
      if (!string.IsNullOrEmpty(texto))
      {
        byte[] result;
        result = SHA512.HashData(Encoding.UTF8.GetBytes(string.Concat(texto, "(qhy=9S#4X$Mg(8M")));
        textoCifrado = BitConverter.ToString(result).Replace("-", string.Empty);
      }
      return textoCifrado;
    }
    
    public string DecodificarJwtToken(string token)
    {
      string valorToken = "";
      var tokenHandler = new JwtSecurityTokenHandler();
      var jsonToken = tokenHandler.ReadJwtToken(token);
      valorToken = jsonToken.Claims.First(claim => claim.Type == "context").Value;
      return valorToken;
    }
  }
}
