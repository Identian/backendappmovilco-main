using Aplicacion.Dto.Solicitudes;
using Dominio.Entidad.Solicitudes;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class SolicitudesCompartidasEmpresaAutenticacion
  {
    public SolicitudValidarClaveSecretaDto DatosBearerTokenExisteValidarClaveSecretaDto = new();
    public SolicitudValidarClaveSecreta DatosBearerTokenExisteValidarClaveSecreta = new();

    private void InicializarSolicitudesDto()
    {
      DatosBearerTokenExisteValidarClaveSecretaDto = new()
      {
        ClaveSecreta = ConstantesCompartidasFacturacion.ClaveSecretaValida
      };
    }

    private void InicializarSolicitudes()
    {
      DatosBearerTokenExisteValidarClaveSecreta = new()
      {
        ClaveSecreta = ConstantesCompartidasFacturacion.ClaveSecretaValida
      };
    }

    public SolicitudesCompartidasEmpresaAutenticacion()
    {
      InicializarSolicitudesDto();
      InicializarSolicitudes();
    }
  }
}
