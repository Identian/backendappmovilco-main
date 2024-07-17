namespace Dominio.Entidad.Respuestas
{
  public class RespuestaObtenerTokensEmpresa
  {
    public bool Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? TokenEnterprise { get; set; }
    public string? TokenPassword { get; set; }
  }
}
