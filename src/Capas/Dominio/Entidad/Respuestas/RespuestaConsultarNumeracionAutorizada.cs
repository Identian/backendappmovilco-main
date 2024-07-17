using Dominio.Entidad.Documentos;

namespace Dominio.Entidad.Respuestas
{
  public class RespuestaConsultarNumeracionAutorizada
  {

    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public NumeracionAutorizada? Datos { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
