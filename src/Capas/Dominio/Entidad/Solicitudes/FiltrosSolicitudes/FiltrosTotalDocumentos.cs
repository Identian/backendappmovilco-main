namespace Dominio.Entidad.Solicitudes.FiltrosSolicitudes
{
  public class FiltrosTotalDocumentos
  {
    public string? Tipo { get; set; }

    public string? Anio { get; set; }

    public IEnumerable<string>? OrigenFacturacion { get; set; }
  }
}
