namespace Aplicacion.Entidad.Documentos
{
  public class DatosDelTransportistaDto
  {
    public string? IndicadordeCuidado { get; set; }
    public string? IndicadordeAtencion { get; set; }
    public string? NombreResponsableEntrega { get; set; }
    public DireccionBaseReferenciaDto? DireccionResponsableEntrega { get; set; }
    public string? TipoIdentificacion { get; set; }
    public string? NumeroIdentificacion { get; set; }
    public string? NumeroIdentificacionDV { get; set; }
    public IEnumerable<ObligacionesBaseDto>? ResponsabilidadesRut { get; set; }
    public IEnumerable<TributosDto>? DetallesTributarios { get; set; }
    public DireccionBaseReferenciaDto? TransportadorDireccion { get; set; }
    public string? TransportadorNombre { get; set; }
    public string? TransportadorTipoIdentificacion { get; set; }
    public string? TransportadorNumeroDocumento { get; set; }
    public string? TransportadorNumeroDocumentoDV { get; set; }
    public string? NumeroMatriculaMercantil { get; set; }
    public string? PrefijoFacturacion { get; set; }
    public string? NombreContacto { get; set; }
    public string? Telefax { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Nota { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
