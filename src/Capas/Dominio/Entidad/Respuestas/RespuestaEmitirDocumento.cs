namespace Dominio.Entidad.Respuestas
{
  public class RespuestaEmitirDocumento
  {
    public int Codigo { get; set; }
    public string? Mensaje { get; set; }
    public string? Cufe { get; set; }
    public string? TipoCufe { get; set; }
    public string? ConsecutivoDocumento { get; set; }
    public string? FechaRespuesta { get; set; }
    public string? Resultado { get; set; }
    public string? Xml { get; set; }
    public string? Hash { get; set; }
    public string? Nombre { get; set; }
    public string? Qr { get; set; }
    public bool EsValidoDian { get; set; }
    public IEnumerable<string>? MensajesValidacion { get; set; }
    public IEnumerable<string>? ReglasValidacionDIAN { get; set; }
    public IEnumerable<string>? ReglasNotificacionDIAN { get; set; }
    public string? FechaAceptacionDIAN { get; set; }
  }
}
