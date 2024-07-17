using Aplicacion.Dto.Empresas;
using Aplicacion.Dto.EstadoDocumento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dto.Respuestas
{
    public class RespuestaConsultarEstadoDocumentoDto
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public string? Plataforma { get; set; }
    public EstadoDocumentoFacturacionDto? Documento { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}
