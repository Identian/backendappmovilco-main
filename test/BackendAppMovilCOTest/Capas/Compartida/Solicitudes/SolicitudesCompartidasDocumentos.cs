using Aplicacion.Dto.Solicitudes;
using Dominio.Entidad.Solicitudes;

namespace BackendAppMovilCOTest.Capas.Compartida.Solicitudes
{
  public class SolicitudesCompartidasDocumentos
  {
    public SolicitudConsultarDocumentosDto solicitudConsultarDocumentosDto { get; set; }
    public SolicitudReporteEnLinea solicitudReporteEnLinea { get; set; }

    public void InicializarSolicitudes()
    {
      SolicitudesConsultarDocumentos();
    }

    public void SolicitudesConsultarDocumentos()
    {
      solicitudConsultarDocumentosDto = new()
      {
        Sistema = "1",
        FormatoRequerido = "json",
        Filtros = new()
        {
          FechaInicio = "2023-02-01 00:00:00",
          FechaHasta = "2023-02-28 23:59:59"
        }
      };

      solicitudReporteEnLinea = new()
      {
        Sistema = "1",
        FormatoRequerido = "json",
        Filtros = new()
        {
          FechaInicio = "2023-02-01 00:00:00",
          FechaHasta = "2023-02-28 23:59:59"
        }
      };
    }

    public SolicitudesCompartidasDocumentos()
    {
      InicializarSolicitudes();
    }
  }
}
