namespace Aplicacion.Dto.Respuestas
{
  public class RespuestaEnviarCorreoIndividualDto
  {
    public int Codigo { get; set; }

    public string? Mensaje { get; set; }

    public string? Resultado { get; set; }

    public IEnumerable<string>? Errores { get; set; }
  }
}
