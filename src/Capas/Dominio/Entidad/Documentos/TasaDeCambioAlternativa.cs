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

namespace Dominio.Entidad.Documentos
{
  public class TasaDeCambioAlternativa
  {
    public string? MonedaOrigen { get; set; }
    public string? BaseMonedaOrigen { get; set; }
    public string? MonedaDestino { get; set; }
    public string? BaseMonedaDestino { get; set; }
    public string? TasaDeCambio { get; set; }
    public string? FechaDeTasaDeCambio { get; set; }
    public string? IndicadorDeTasa { get; set; }
    public string? OperadorCalculo { get; set; }
    public IEnumerable<Extensible>? Extras { get; set; }
  }
}
