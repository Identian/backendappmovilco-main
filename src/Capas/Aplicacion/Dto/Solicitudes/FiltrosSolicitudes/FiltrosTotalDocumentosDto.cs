namespace Aplicacion.Dto.Solicitudes.FiltrosSolicitudes
{
  public class FiltrosTotalDocumentosDto
  {
    public string? Tipo { get; set; }

    public string? Anio { get; set; }

    public IEnumerable<string>? OrigenFacturacion { get; set; }
  }
}
