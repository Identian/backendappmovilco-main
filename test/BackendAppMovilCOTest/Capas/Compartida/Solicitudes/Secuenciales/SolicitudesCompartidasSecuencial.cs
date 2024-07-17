using Aplicacion.Dto.Solicitudes;
using Dominio.Entidad.Solicitudes;

namespace BackendAppMovilCOTest.Capas.Compartida.Solicitudes.Secuenciales
{
  public class SolicitudesCompartidasSecuencial
  {
    public SolicitudSeleccionarSecuencialDto? datosValidosSeleccionarSecuencialDto { get; set; }
    public SolicitudSeleccionarSecuencial? datosValidosSeleccionarSecuencial { get; set; }


    public void InicializarSolicitudes()
    {
      SolicitudesConsultarDocumentos();
      SolicitudesConsultarDocumentosDto();
    }

    public void SolicitudesConsultarDocumentos()
    {
      datosValidosSeleccionarSecuencial = new()
      {
        IdNumeracion = ConstantesCompartidasFacturacion.IdNumeracionValido,
        Seleccionado = "1",
        Referencia = "Nuevo"
      };
    }
    public void SolicitudesConsultarDocumentosDto()
    {
      datosValidosSeleccionarSecuencialDto = new()
      {
        IdNumeracion = ConstantesCompartidasFacturacion.IdNumeracionValido,
        Seleccionado = "1",
        Referencia = "Nuevo"
      };
    }

    public SolicitudesCompartidasSecuencial()
    {
      InicializarSolicitudes();
    }
  }
}
