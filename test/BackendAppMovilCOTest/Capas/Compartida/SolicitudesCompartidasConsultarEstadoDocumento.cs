using Aplicacion.Dto.Solicitudes;
using Dominio.Entidad.Documentos;
using Dominio.Entidad.Solicitudes;

namespace BackendAppMovilCOTest.Capas.Compartida
{


  public class SolicitudesCompartidasConsultarEstadoDocumento
  {
    public SolicitudConsultarEstadoDocumentoFacturacion SolicitudConsultarDocumentoValido { get; set; }
    public SolicitudConsultarEstadoDocumentoFacturacionDto SolicitudConsultarDocumentoValidoDto { get; set; }
    public SolicitudConsultarEstadoDocumentoFacturacion SolicitudConsultarDocumentoInvalido { get; set; }
    public SolicitudConsultarEstadoDocumentoFacturacionDto SolicitudConsultarDocumentoInvalidoDto { get; set; }
    public SolicitudConsultarEstadoDocumentoFacturacion SolicitudConsultarPlataformaNoDisponible { get; set; }
    public SolicitudConsultarEstadoDocumentoFacturacionDto SolicitudConsultarPlataformaNoDisponibleDto { get; set; }


    private void InicializarSolicitudesDto()
    {
      SolicitudConsultarDocumentoValidoDto = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA",
        Consecutivo = "ARF84",
      };

      SolicitudConsultarDocumentoInvalido = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA",
        Consecutivo = "ARF841",
      };

      SolicitudConsultarPlataformaNoDisponible = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "DIAN",
        Consecutivo = "ARF84",
      };
    }

    private void InicializarSolicitudes()
    {
      SolicitudConsultarDocumentoValido = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA",
        Consecutivo = "ARF84",
      };

      SolicitudConsultarDocumentoInvalidoDto = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA",
        Consecutivo = "ARF841",
      };

      SolicitudConsultarPlataformaNoDisponibleDto = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "DIAN",
        Consecutivo = "ARF84",
      };

    }

    public SolicitudesCompartidasConsultarEstadoDocumento()
    {
      InicializarSolicitudesDto();
      InicializarSolicitudes();
    }
  }
}
