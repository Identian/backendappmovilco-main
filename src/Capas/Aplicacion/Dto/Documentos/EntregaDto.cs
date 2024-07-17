using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Aplicacion.Entidad.Documentos
{
  public class EntregaDto
  {
    public string? FechaEfectivaSalida { get; set; }
    public DireccionBaseReferenciaDto? DireccionEntrega { get; set; }
    public DatosDelTransportistaDto? DatosTransportistas { get; set; }
    public string? IdentificacionTransporte { get; set; }
    public string? MatriculaTransporte { get; set; }
    public string? FechaSolicitada { get; set; }
    public string? FechaEstimada { get; set; }
    public string? FechaReal { get; set; }
    public DireccionBaseReferenciaDto? DireccionDespacho { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
