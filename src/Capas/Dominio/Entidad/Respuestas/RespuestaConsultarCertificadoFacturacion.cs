namespace Dominio.Entidad.Respuestas
{
  public class RespuestaConsultarCertificadoFacturacion
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public CertificadoFacturacion? Certificado { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
