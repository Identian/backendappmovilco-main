using Aplicacion.Dto.Solicitudes;
using Dominio.Entidad.Solicitudes;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class SolicitudesCompartidasEmpresa
  {
    public SolicitudConsultarFacturacionDto DatosExistentesConsultarEmpresaDto = new();
    public SolicitudConsultarFacturacionDto DatosNitNoExisteConsultarEmpresaDto = new();
    public SolicitudConsultarFacturacionDto PlataformaNoDisponibleConsultarEmpresaDto = new();
    public SolicitudConsultarFacturacionDto PlataformaInvalidaConsultarEmpresaDto = new();
    public SolicitudValidarClaveSecretaDto DatosBearerTokenExisteValidarClaveSecretaDto = new();
    public SolicitudConsultarFacturacion DatosExistentesConsultarEmpresa = new();
    public SolicitudConsultarFacturacion DatosNitNoExisteConsultarEmpresa = new();
    public SolicitudConsultarFacturacion PlataformaNoDisponibleConsultarEmpresa = new();
    public SolicitudConsultarFacturacion DatosIdEmpresaNoExisteConsultarEmpresa = new();
    public SolicitudConsultarFacturacion DatosIdEmpresaNoExisteConsultarEmpresaDominio = new();
    public SolicitudValidarClaveSecreta DatosBearerTokenExisteValidarClaveSecreta = new();

    private void InicializarSolicitudesDto()
    {
      DatosExistentesConsultarEmpresaDto = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
        TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido,
        Plataforma = ConstantesCompartidasFacturacion.PlataformaTFHKA
      };

      DatosNitNoExisteConsultarEmpresaDto = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaInexistente,
        Nit = ConstantesCompartidasFacturacion.NitInexistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaInvalido,
        TokenClave = ConstantesCompartidasFacturacion.TokenClaveInvalido,
        Plataforma = ConstantesCompartidasFacturacion.PlataformaTFHKA
      };

      PlataformaNoDisponibleConsultarEmpresaDto = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
        TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido,
        Plataforma = ConstantesCompartidasFacturacion.PlataformaNoDisponible
      };

      PlataformaInvalidaConsultarEmpresaDto = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
        TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido,
        Plataforma = "ASDL"
      };

      DatosBearerTokenExisteValidarClaveSecretaDto = new()
      {
        ClaveSecreta = ConstantesCompartidasFacturacion.ClaveSecretaValida
      };
    }

    private void InicializarSolicitudes()
    {
      DatosExistentesConsultarEmpresa = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
        TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido,
        Plataforma = ConstantesCompartidasFacturacion.PlataformaTFHKA
      };

      DatosNitNoExisteConsultarEmpresa = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaInexistente,
        Nit = ConstantesCompartidasFacturacion.NitInexistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaInvalido,
        TokenClave = ConstantesCompartidasFacturacion.TokenClaveInvalido,
        Plataforma = ConstantesCompartidasFacturacion.PlataformaTFHKA
      };

      PlataformaNoDisponibleConsultarEmpresa = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
        TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido,
        Plataforma = ConstantesCompartidasFacturacion.PlataformaNoDisponible
      };

      DatosIdEmpresaNoExisteConsultarEmpresa = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaInexistente,
        Nit = ConstantesCompartidasFacturacion.NitInexistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaInvalido,
        TokenClave = null,
        Plataforma = ConstantesCompartidasFacturacion.PlataformaTFHKA
      };

      DatosIdEmpresaNoExisteConsultarEmpresaDominio = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaInexistente,
        Nit = ConstantesCompartidasFacturacion.NitInexistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaInvalido,
        TokenClave = null,
        Plataforma = ConstantesCompartidasFacturacion.PlataformaTFHKA
      };

      DatosBearerTokenExisteValidarClaveSecreta = new()
      {
        ClaveSecreta = ConstantesCompartidasFacturacion.ClaveSecretaValida
      };
    }

    public SolicitudesCompartidasEmpresa()
    {
      InicializarSolicitudesDto();
      InicializarSolicitudes();
    }
  }
}
