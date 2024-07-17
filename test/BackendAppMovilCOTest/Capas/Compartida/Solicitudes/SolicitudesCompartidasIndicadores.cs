using Aplicacion.Dto.Solicitudes.FiltrosSolicitudes;
using Dominio.Entidad.Solicitudes.FiltrosSolicitudes;

namespace BackendAppMovilCOTest.Capas.Compartida.Solicitudes
{
  public class SolicitudesCompartidasIndicadores
  {
    public FiltrosTotalDocumentosDto? DatosExistentesConsultarTotalDocumentosDto;
    public FiltrosTotalDocumentosDto? DatosVaciosConsultarTotalDocumentosDto;
    public FiltrosTotalDocumentos? DatosExistentesConsultarTotalDocumentos;
    public FiltrosTotalDocumentos? DatosVaciosConsultarTotalDocumentos;

    public SolicitudesCompartidasIndicadores()
    {
      Inicializar();
    }

    public void Inicializar()
    {
      InicializarConsultarTotalDocumentos();
    }

    private void InicializarConsultarTotalDocumentos()
    {
      #region Solicitudes Dto
      DatosExistentesConsultarTotalDocumentosDto = new()
      {
        Anio = DateTime.Now.ToString("yyyy"),
        OrigenFacturacion = "1,2,3".Split(",")
      };

      DatosVaciosConsultarTotalDocumentosDto = new()
      {
        Anio = "2000",
        OrigenFacturacion = "1,2,3".Split(",")
      };
      #endregion

      #region Solicitudes
      DatosExistentesConsultarTotalDocumentos = new()
      {
        Anio = DateTime.Now.ToString("yyyy"),
        OrigenFacturacion = "1,2,3".Split(",")
      };

      DatosVaciosConsultarTotalDocumentos = new()
      {
        Anio = "2000",
        OrigenFacturacion = "1,2,3".Split(",")
      };
      #endregion
    }
  }
}
