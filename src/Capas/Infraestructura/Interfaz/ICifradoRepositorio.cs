namespace Infraestructura.Interfaz
{
  public interface ICifradoRepositorio
  {
    public string CifrarTexto(string texto);
    public string DecodificarJwtToken(string token);
  }
}
