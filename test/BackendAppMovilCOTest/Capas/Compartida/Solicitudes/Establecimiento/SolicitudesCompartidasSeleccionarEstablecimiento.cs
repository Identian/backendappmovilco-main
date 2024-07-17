using Aplicacion.Dto.Solicitudes;
using Dominio.Entidad.Solicitudes;

namespace BackendAppMovilCOTest.Capas.Compartida.Solicitudes.Establecimiento
{
  public class SolicitudesCompartidasSeleccionarEstablecimiento
  {
    public SolicitudSeleccionarEstablecimientoDto? solicitudSeleccionarEstablecimientoDto { get; set; }
    public SolicitudSeleccionarEstablecimiento? solicitudSeleccionarEstablecimiento { get; set; }

    public void InicializarSolicitudes()
    {
      SolicitudesConsultarDocumentos();
    }

    public void SolicitudesConsultarDocumentos()
    {
      solicitudSeleccionarEstablecimientoDto = new()
      {
        IdEstablecimiento = ConstantesCompartidasFacturacion.IdEstablecimientoValido,
        Seleccionado = "1",
        Referencia = "Nuevo"

      };

      solicitudSeleccionarEstablecimiento = new()
      {
        IdEstablecimiento = ConstantesCompartidasFacturacion.IdEstablecimientoValido,
        Seleccionado = "1",
        Referencia = "Nuevo"

      };
    }

    public SolicitudesCompartidasSeleccionarEstablecimiento()
    {
      InicializarSolicitudes();
    }
  }
}
