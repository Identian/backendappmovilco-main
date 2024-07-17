namespace Dominio.Entidad.EstadoDocumento

{
  public class EstadoDocumentoFacturacion
  {
    public int Codigo { get; set; }
    public string? Mensaje { get; set; }
    public string? Resultado { get; set; }
    public string? FechaDocumento { get; set; }
    public string? Cufe { get; set; }
    public int EstatusDocumento { get; set; }
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
    public bool PoseeRepresentacionGrafica { get; set; }
    public bool PoseeAdjuntos { get; set; }
    public bool AceptacionFisica { get; set; }
    public List<HistorialEntrega>? HistorialDeEntregas { get; set; }
    public bool EsValidoDIAN { get; set; }
    public string? TrackID { get; set; }
    public string? CadenaCodigoQR { get; set; }
    public List<Evento>? Eventos { get; set; }
    public string? FechaAceptacionDIAN { get; set; }
    public string? AcuseEstatus { get; set; }
    public string? AcuseRespuesta { get; set; }
    public string? AcuseComentario { get; set; }
    public string? AcuseResponsable { get; set; }
  }
}
