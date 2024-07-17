namespace Dominio.Entidad.Solicitudes
{
  public class SolicitudConClaveSecretaFacturacion
  {
    public string? Nit { get; set; }
    public string? TokenEmpresa { get; set; }
    public string? TokenClave { get; set; }
    public string? ClaveSecreta { get; set; }
  }
}
