using Aplicacion.Dto.Dispositivos;
using Aplicacion.Dto.Solicitudes.Dispositivos;
using Dominio.Entidad.Dispositivos;
using Dominio.Entidad.Solicitudes;

namespace BackendAppMovilCOTest.Capas.Compartida.Solicitudes.Dispositivos
{
  public class SolicitudesCompartidasDispositivos
  {
    public DispositivoDto DatosValidosCrearDispositivoDto = new();
    public Dispositivo DatosValidosCrearDispositivo = new();
    public SolicitudAsociarAlias DatosValidosAsociarAlias = new();
    public SolicitudAsociarAliasDto DatosValidosAsociarAliasDto = new();

    public SolicitudConsultarSuscripcionDispositivoDto ConsultarSuscripcionDispositivoDto = new();

    private void InicializarSolicitudesDto()
    {
      DatosValidosCrearDispositivoDto = new()
      {
        Serial = null,
        SerialAntiguo = null,
        SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido,
        Nombre = "samsum",
        Marca = null,
        Modelo = null,
        Tipo = null,
        TipoApp = ConstantesCompartidasFacturacion.IdTipoAppExistente,
        Version = null
      };

      #region Consultar Suscripcion Dispositivo
      ConsultarSuscripcionDispositivoDto = new()
      {
        SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionExistente
      };
      #endregion

      #region Asociar Alias
      DatosValidosAsociarAliasDto = new()
      {
        SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido,
        Alias = ConstantesCompartidasFacturacion.AliasDispositivoValido
      };
      #endregion
    }

    private void InicializarSolicitudes()
    {
      DatosValidosCrearDispositivo = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Serial = null,
        SerialAntiguo = null,
        SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido,
        Nombre = "samsum",
        Marca = null,
        Modelo = null,
        Tipo = null,
        TipoApp = ConstantesCompartidasFacturacion.IdTipoAppExistente,
        Version = null
      };

      DatosValidosAsociarAlias = new()
      {
        SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido,
        Alias = ConstantesCompartidasFacturacion.AliasDispositivoValido
      };
    }

    public SolicitudesCompartidasDispositivos()
    {
      InicializarSolicitudesDto();
      InicializarSolicitudes();
    }
  }
}
