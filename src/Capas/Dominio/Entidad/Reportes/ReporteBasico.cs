namespace Dominio.Entidad.Reportes
{
  public class ReporteBasico
  {
    public string? TipoAplicacion { get; set; } // Origen
    public string? DescripcionDocumento { get; set; }
    public string? FechaEmision { get; set; } // issue_date 
    public string? NumeroDocumento { get; set; } // Document_id
    public string? Cufe { get; set; }
    public string? TipoCufe { get; set; }
    public string? IdFactura { get; set; } // id_invoice}
    public string? RazonSocialAdquiriente { get; set; }
    public string? NumeroIdentificacionAdquiriente { get; set; }
    public string? MontoTotal { get; set; }
    public string? EstatusHka { get; set; }
    public string? MensajeHka { get; set; }
    public string? DianEstatus { get; set; }
    public string? DianEstatusDescripcion { get; set; }
    public string? DianEstatusFecha { get; set; }
    public string? ValidacionesDian { get; set; }
    public string? NotificacionesDian { get; set; }
    public string? FechaValidacionDian { get; set; }
    public string? CodigoSucursal { get; set; }
    public string? Estado_de_acuse { get; set; }
    public string? ComentarioAcuse { get; set; }
    public string? EntregaEmail { get; set; }
    public string? EntregaEstado { get; set; }
    public string? EntregaMensaje { get; set; }
    public string? FechaLectura { get; set; }
    public string? FechaAcuse { get; set; }
  }
}
