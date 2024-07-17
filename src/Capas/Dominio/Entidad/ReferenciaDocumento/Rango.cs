using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.ReferenciaDocumento
{
  public class Rango
  {
    public int IdRango { get; set; }
    public string? RangoInicio { get; set; }
    public string? RangoFin { get; set; }
    public string? Prefijo { get; set; }
    public string? Documenid { get; set; }
  }
}
