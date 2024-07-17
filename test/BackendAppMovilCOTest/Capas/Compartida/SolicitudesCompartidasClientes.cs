using Aplicacion.Dto.Solicitudes;
using Dominio.Entidad.Solicitudes;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class SolicitudesCompartidasClientes
  {
    public SolicitudConsultarFacturacionDto DatosExistentesConsultarClientesDto = new();
    public SolicitudConsultarFacturacionDto DatosNitNoExisteConsultarClientesDto = new();
    public SolicitudConsultarFacturacionDto PlataformaNoDisponibleConsultarClientesDto = new();
    public SolicitudConsultarFacturacion DatosExistentesConsultarClientes = new();
    public SolicitudConsultarFacturacion DatosNitNoExisteConsultarClientes = new();
    public SolicitudConsultarFacturacion PlataformaNoDisponibleConsultarClientes = new();
    public SolicitudConsultarFacturacion DatosIdEmpresaNoExisteConsultarClientes = new();

  


    private void InicializarSolicitudesDto()
    {
      DatosExistentesConsultarClientesDto = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA"
      };

      DatosNitNoExisteConsultarClientesDto = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitInexistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA"
      };

      PlataformaNoDisponibleConsultarClientesDto = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = ConstantesCompartidasFacturacion.PlataformaNoDisponible
      };
    }

    private void InicializarSolicitudes()
    {
      DatosExistentesConsultarClientes = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA"
      };

      DatosNitNoExisteConsultarClientes = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitInexistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = "TFHKA"
      };

      PlataformaNoDisponibleConsultarClientes = new()
      {
        IdEmpresa = "1",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = "1",
        TokenClave = "1",
        Plataforma = ConstantesCompartidasFacturacion.PlataformaNoDisponible
      };

      DatosIdEmpresaNoExisteConsultarClientes = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaInexistente,
        Nit = "1",
        TokenEmpresa = "1",
        TokenClave = null,
        Plataforma = "TFHKA"
      };
    }

   



    public SolicitudesCompartidasClientes()
    {
      InicializarSolicitudesDto();
      InicializarSolicitudes();
  
    
    }
  }
}
