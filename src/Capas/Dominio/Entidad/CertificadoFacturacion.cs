﻿namespace Dominio.Entidad
{
  public class CertificadoFacturacion
  {
    public string? Serie { get; set; }
    public string? ValidoDesde { get; set; }
    public string? ValidoHasta { get; set; }
    public string? RazonSocial { get; set; }
    public string? Proveedor { get; set; }
  }
}