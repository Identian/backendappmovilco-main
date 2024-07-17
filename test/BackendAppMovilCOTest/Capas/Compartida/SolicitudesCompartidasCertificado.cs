using Aplicacion.Dto.Solicitudes;
using Dominio.Entidad.Solicitudes;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class SolicitudesCompartidasCertificado
  {
    public SolicitudConsultarFacturacionDto DatosExistentesConsultarCertificadoDto = new();
    public SolicitudConsultarFacturacionDto DatosNitNoExisteConsultarCertificadoDto = new();
    public SolicitudConsultarFacturacion DatosExistentesConsultarCertificado = new();
    public SolicitudConsultarFacturacion DatosNitNoExisteConsultarCertificado = new();

    private void InicializarSolicitudesDto()
    {
      DatosExistentesConsultarCertificadoDto = new()
      {
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1"
      };

      DatosNitNoExisteConsultarCertificadoDto = new()
      {
        Nit = ConstantesCompartidasFacturacion.NitInexistente,
        TokenEmpresa = "1",
        TokenClave = "1"
      };
    }

    private void InicializarSolicitudes()
    {
      DatosExistentesConsultarCertificado = new()
      {
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1"
      };

      DatosNitNoExisteConsultarCertificado = new()
      {
        Nit = ConstantesCompartidasFacturacion.NitInexistente,
        TokenEmpresa = "1",
        TokenClave = "1"
      };
    }

    public SolicitudesCompartidasCertificado()
    {
      InicializarSolicitudesDto();
      InicializarSolicitudes();
    }
  }
}
