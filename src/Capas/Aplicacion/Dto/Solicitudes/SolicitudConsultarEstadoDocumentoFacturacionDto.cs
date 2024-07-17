namespace Aplicacion.Dto.Solicitudes
{
  public class SolicitudConsultarEstadoDocumentoFacturacionDto
  {
    public string? IdEmpresa { get; set; }
    public string? Nit { get; set; }
    public string? TokenEmpresa { get; set; }
    public string? TokenClave { get; set; }
    public string? Plataforma { get; set; }
    public string? Consecutivo { get; set;}
  }
}
