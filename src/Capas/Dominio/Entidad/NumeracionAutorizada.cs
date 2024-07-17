using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad
{
  public class NumeracionAutorizada
  {
    public string? IdNumeracion { get; set; }
    public string? NumeroResolucion { get; set; }
    public string? FechaResolucion { get; set; }
    public string? Prefijo { get; set; }
    public string? NumeroDesde { get; set; }
    public string? NumeroHasta { get; set; }
    public string? NumeroInicial { get; set; }
    public string? FechaDesde { get; set; }
    public string? FechaHasta { get; set; }
    public string? ClaveTecnica { get; set; }
    public string? TipoDocumento { get; set; }
    public string? IdEstablecimiento { get; set; }
    public string? TipoServicio { get; set; }
    public string? Modalidad { get; set; }
    public string? TipoAmbienteSecuencial { get; set; }
    public string? TestSetId { get; set; }
    public string? EnvioDian { get; set; }
    public string? Activo { get; set; }
    public string? RangoNumeracion { get; set; }
  }
}
