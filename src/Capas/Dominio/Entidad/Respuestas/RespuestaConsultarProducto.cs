using Dominio.Entidad.Documentos;

namespace Dominio.Entidad.Respuestas
{
  public class RespuestaConsultarProducto
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public FacturaDetalle? Datos { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
