namespace Dominio.Entidad.Solicitudes
{
  public class SolicitudEnviarCorreoIndividual
  {
    public string? Documento { get; set; }

    public string? InvoiceType { get; set; }

    public string? Email { get; set; }

    public int? Adjunto { get; set; }

    public byte? AttachementTrue { get; set; }

    public IEnumerable<string>? Ruta { get; set; }

    public IEnumerable<string>? Formato { get; set; }

    public IEnumerable<string>? Nombre_Display { get; set; }

    public IEnumerable<string>? NombreArchivo { get; set; }

    public IEnumerable<int>? Type { get; set; }
  }
}
