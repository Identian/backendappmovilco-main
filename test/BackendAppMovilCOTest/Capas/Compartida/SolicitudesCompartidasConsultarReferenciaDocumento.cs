using Aplicacion.Dto.Solicitudes;
using Aplicacion.Entidad.Solicitudes;
using Dominio.Entidad.Documentos;
using Dominio.Entidad.Solicitudes;

namespace BackendAppMovilCOTest.Capas.Compartida
{


  public class SolicitudesCompartidasConsultarReferenciaDocumento
  {
    public SolicitudConsultarReferenciaDocumentoFacturacion SolicitudConsultarDocumentoValido1 { get; set; }
    public SolicitudConsultarReferenciaDocumentoFacturacionDto SolicitudConsultarDocumentoValido1Dto { get; set; }
    public SolicitudConsultarReferenciaDocumentoFacturacion SolicitudConsultarDocumentoValido2 { get; set; }
    public SolicitudConsultarReferenciaDocumentoFacturacionDto SolicitudConsultarDocumentoValido2Dto { get; set; }
    public SolicitudConsultarReferenciaDocumentoFacturacion SolicitudConsultarDocumentoInvalido { get; set; }
    public SolicitudConsultarReferenciaDocumentoFacturacionDto SolicitudConsultarDocumentoInvalidoDto { get; set; }
    public SolicitudConsultarReferenciaDocumentoFacturacion SolicitudConsultarPlataformaNoDisponible { get; set; }
    public SolicitudConsultarReferenciaDocumentoFacturacionDto SolicitudConsultarPlataformaNoDisponibleDto { get; set; }


    private void InicializarSolicitudesDto()
    {
      SolicitudConsultarDocumentoValido1Dto = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA",
        IdInvoice = ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido,
        TipoConsulta = ConstantesCompartidasFacturacion.TipoConsulta1,
      };


      SolicitudConsultarDocumentoValido2Dto = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA",
        IdInvoice = ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido,
        TipoConsulta = ConstantesCompartidasFacturacion.TipoConsulta0,
      };



      SolicitudConsultarDocumentoInvalidoDto = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA",
        IdInvoice = "3456",
        TipoConsulta = "1",
      };

      SolicitudConsultarPlataformaNoDisponibleDto = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "DIAN",
        IdInvoice = ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido,
        TipoConsulta = "1",
      };
    }

    private void InicializarSolicitudes()
    {
      SolicitudConsultarDocumentoValido1 = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA",
        IdInvoice = ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido,
        TipoConsulta = ConstantesCompartidasFacturacion.TipoConsulta1,
      };

      SolicitudConsultarDocumentoValido2 = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA",
        IdInvoice = ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido,
        TipoConsulta = ConstantesCompartidasFacturacion.TipoConsulta0,
      };

      SolicitudConsultarDocumentoInvalido = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA",
        IdInvoice = "ARF841",
        TipoConsulta = "1",
      };

      SolicitudConsultarPlataformaNoDisponible = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "DIAN",
        IdInvoice = "ARF84",
        TipoConsulta = "1",
      };

    }

    public SolicitudesCompartidasConsultarReferenciaDocumento()
    {
      InicializarSolicitudesDto();
      InicializarSolicitudes();
    }
  }
}
