using Dominio.Entidad.Documentos;

namespace Dominio.Entidad.Respuestas
{
  public class RespuestaConsultarCliente
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public Cliente? Datos { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
