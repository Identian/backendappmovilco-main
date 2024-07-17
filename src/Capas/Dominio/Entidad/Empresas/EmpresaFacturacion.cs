namespace Dominio.Entidad.Empresas
{
  public class EmpresaFacturacion
  {
    public string? IdEmpresa { get; set; }
    public string? Tipo { get; set; }
    public string? DescripcionTipo { get; set; }
    public string? RazonSocial { get; set; }
    public string? FechaCreacion { get; set; }
    public string? FechaActualizacion { get; set; }
    public string? Entorno { get; set; }
    public string? TokenEmpresa { get; set; }
    public string? TokenClave { get; set; }
    public byte? Activo { get; set; }
    public string? TipoIdentificacion { get; set; }
    public string? DescripcionTipoIdentificacion { get; set; }
    public string? NumeroIdentificacion { get; set; }
    public string? NumeroIdentificacionDv { get; set; }
    public string? NombreComercial { get; set; }
    public byte? TieneIntegracion { get; set; }
    public byte? TieneRecepcion { get; set; }
    public byte? TieneAppMovil { get; set; }
    public byte? TieneMetodo { get; set; }
    public string? Estatus { get; set; }
    public byte? TieneInteroperabilidad { get; set; }
    public byte? EsCasaSoftware { get; set; }
    public byte? TieneConsorcio { get; set; }
    public string? IdHistorial { get; set; }
    public string? CodigoTipoRegimen { get; set; }
    public byte? TieneNomina { get; set; }
    public string? EntornoNomina { get; set; }
    public string? EstatusProveedor { get; set; }
    public byte? TieneRadian { get; set; }
  }
}
