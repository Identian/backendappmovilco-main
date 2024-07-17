namespace Aplicacion.Dto.EstadoDocumento
{
  public class EstadoDocumentoFacturacionDto
  {
    public string? FechaDocumento { get; set; }
    public string? Cufe { get; set; }
    public string? EstatusDocumento { get; set; }
    public string? MensajeDocumento { get; set; }
    public string? TipoDocumento { get; set; }
    public string? DescripcionDocumento { get; set; }
    public string? Consecutivo { get; set; }
    public string? Ambiente { get; set; }
    public string? TipoCufe { get; set; }
    public string? CadenaCufe { get; set; }
    public string? EntregaMetodoDIAN { get; set; }
    public string? DescripcionEstatusDocumento { get; set; }
    public List<string>? ReglasValidacionDIAN { get; set; }
    public string? PoseeRepresentacionGrafica { get; set; }
    public string? PoseeAdjuntos { get; set; }
    public string? AceptacionFisica { get; set; }
    public List<HistorialEntregaDto>? HistorialDeEntregas { get; set; }
    public string? EsValidoDIAN { get; set; }
    public string? TrackID { get; set; }
    public string? CadenaCodigoQR { get; set; }
    public List<EventoDto>? Eventos { get; set; }
    public string? FechaAceptacionDIAN { get; set; }
    public string? AcuseEstatus { get; set; }
    public string? AcuseRespuesta { get; set; }
    public string? AcuseComentario { get; set; }
    public string? AcuseResponsable { get; set; }
  }
}
