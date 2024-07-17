namespace Dominio.Entidad.Documentos
{
  public class Cliente
  {
    public string? IdCliente { get; set; }
    public string? NombreRazonSocial { get; set; }
    public string? TipoPersona { get; set; }
    public string? SegundoNombre { get; set; }
    public string? Apellido { get; set; }
    public string? TipoIdentificacion { get; set; }
    public string? NumeroDocumento { get; set; }
    public string? NumeroIdentificacionDv { get; set; }
    public string? CanalDeEntrega { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public string? Notificar { get; set; }
    public IEnumerable<Destinatario>? Destinatario { get; set; }
    public string? NombreComercial { get; set; }
    public IEnumerable<ObligacionesBase>? ResponsabilidadesRut { get; set; }
    public IEnumerable<Tributos>? DetallesTributarios { get; set; }
    public DireccionBase? DireccionCliente { get; set; }
    public DireccionBase? DireccionFiscal { get; set; }
    public InformacionLegal? InformacionLegalCliente { get; set; }
    public string? NombreRegistroRut { get; set; }
    public string? NumeroIdentificacion { get; set; }
    public string? NombreContacto { get; set; }
    public string? Telefax { get; set; }
    public string? Nota { get; set; }
    public string? ActividadEconomicaCiiu { get; set; }
    public IEnumerable<Extensible>? Extras { get; set; }
  }
}
