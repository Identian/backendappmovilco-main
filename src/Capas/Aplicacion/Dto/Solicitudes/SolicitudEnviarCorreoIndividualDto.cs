namespace Aplicacion.Dto.Solicitudes
{
  public class SolicitudEnviarCorreoIndividualDto
  {
    public string? Documento { get; set; }

    public string? Correo { get; set; }

    #region Campos opcionales. Se colocan para mantener la compatibilidad con la solicitud original que espera Delivery

    public string? TipoDocumento { get; set; }

    public string? Adjunto { get; set; }

    public string? TieneAdjuntos { get; set; }

    public IEnumerable<string>? Ruta { get; set; }

    public IEnumerable<string>? Formato { get; set; }

    public IEnumerable<string>? NombreParaMostrar { get; set; }

    public IEnumerable<string>? NombreArchivo { get; set; }

    public IEnumerable<string>? Tipo { get; set; }
    #endregion Campos opcionales
  }
}
