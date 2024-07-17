using Dominio.Entidad.Solicitudes.FiltrosSolicitudes;

namespace Dominio.Entidad.Solicitudes
{
  public class SolicitudConsultarTotalDocumentos
  {
    public string? Nit { get; set; }

    public string? TokenEmpresa { get; set; }

    public string? TokenClave { get; set; }

    public FiltrosTotalDocumentos? Filtros { get; set; }
  }
}
