using Dominio.Entidad.Empresas;
using Dominio.Entidad.Respuestas;
using Newtonsoft.Json.Linq;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasEmpresaAutenticacion
  {

    public EmpresaAutenticacion EmpresaAutenticacionExitosa { get; set; }
    public EmpresaAutenticacion EmpresaAutenticacionExitosaBuscarDespuesDeTimeOutRedisCache { get; set; }
    public EmpresaAutenticacion EmpresaAutenticacionExitosaTimeOutRedisCache { get; set; }
    public EmpresaAutenticacion EmpresaAutenticacionExitosaActivoAppInvalido { get; set; }
    public EmpresaAutenticacion EmpresaAutenticacionExitosaActivoAppUsuarioInvalido { get; set; }
    public EmpresaAutenticacion EmpresaAutenticacionExitosaEmpresaInvalidaGestionAccesoApp { get; set; }
    public EmpresaAutenticacion UsuarioAutenticacionExitosaUsuarioNoExisteGestionAccesoApp { get; set; }
    public EmpresaAutenticacion EmpresaAutenticacionErronea { get; set; }

    public RespuestaValidarClaveSecreta RespuestaDatosValidosValidarClaveSecreta { get; set; }
    public RespuestaValidarClaveSecreta RespuestaDatosInvalidosValidarClaveSecreta { get; set; }
    public RespuestaValidarClaveSecreta SeHaCerradoSesionConsultarUsuario { get; set; }

    public RespuestaConsultarClavesEmpresa RespuestaConsultarClavesEmpresaExitosa { get; set; }
    public RespuestaConsultarClavesEmpresa RespuestaConsultarClavesEmpresaExitosaSinClaveSecreta { get; set; }
    public RespuestaValidarClaveSecreta RespuestaDatosInvalidosClaveSecretaInactiva { get; set; }

    public RespuestasCompartidasEmpresaAutenticacion()
    {
      InicializarRespuestas();
    }

    private void InicializarRespuestas()
    {
      EmpresaAutenticacionExitosa = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
        TokenClave = null,
        NitEmpresa = ConstantesCompartidasFacturacion.NitExistente,
        IdEsquemaEmpresa = null,
        Entorno = null
      };

      EmpresaAutenticacionExitosaBuscarDespuesDeTimeOutRedisCache = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
        TokenClave = null,
        NitEmpresa = ConstantesCompartidasFacturacion.NitExistente,
        IdEsquemaEmpresa = null,
        Entorno = null
      };

      EmpresaAutenticacionExitosaTimeOutRedisCache = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaInvalida,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
        TokenClave = null,
        NitEmpresa = ConstantesCompartidasFacturacion.NitExistente,
        IdEsquemaEmpresa = null,
        Entorno = null
      };

      EmpresaAutenticacionExitosaActivoAppInvalido = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistenteActivoAppInvalido,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
        TokenClave = null,
        NitEmpresa = ConstantesCompartidasFacturacion.NitExistente,
        IdEsquemaEmpresa = null,
        Entorno = null
      };

      EmpresaAutenticacionExitosaActivoAppUsuarioInvalido = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
        TokenClave = null,
        NitEmpresa = ConstantesCompartidasFacturacion.NitExistente,
        IdEsquemaEmpresa = null,
        Entorno = null
      };

      EmpresaAutenticacionExitosaEmpresaInvalidaGestionAccesoApp = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaInvalidaGestionAccesoApp,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
        TokenClave = null,
        NitEmpresa = ConstantesCompartidasFacturacion.NitExistente,
        IdEsquemaEmpresa = null,
        Entorno = null
      };

      EmpresaAutenticacionErronea = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaInexistente,
        TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
        TokenClave = null,
        NitEmpresa = ConstantesCompartidasFacturacion.NitExistente,
        IdEsquemaEmpresa = null,
        Entorno = null
      };

      RespuestaDatosValidosValidarClaveSecreta = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoConsultaExitosaClaveSecreta,
        Resultado = ConstantesCompartidasFacturacion.ResultadoClaveSecretaEncontrada,
        Mensaje = ConstantesCompartidasFacturacion.MensajeClaveSecretaEncontrada,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Errores = null
      };

      RespuestaDatosInvalidosValidarClaveSecreta = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoConsultaInvalidaClaveSecreta,
        Resultado = ConstantesCompartidasFacturacion.ResultadoClaveSecretaInvalida,
        Mensaje = ConstantesCompartidasFacturacion.MensajeClaveSecretaInvalida,
        IdEmpresa = null,
        Errores = null
      };

      RespuestaDatosInvalidosClaveSecretaInactiva = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoConsultaInvalidaClaveSecreta,
        Resultado = ConstantesCompartidasFacturacion.ResultadoClaveSecretaInvalida,
        Mensaje = ConstantesCompartidasFacturacion.MensajeClaveSecretaInactiva,
        IdEmpresa = null,
        Errores = null
      };

      SeHaCerradoSesionConsultarUsuario = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoSessionCerrada,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSesionCerrada,
        IdEmpresa = null,
        Errores = null
      };

      RespuestaConsultarClavesEmpresaExitosa = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        ClavesEmpresa = JToken.FromObject(new
        {
          NumeroIdentificacion = ConstantesCompartidasFacturacion.NitExistente,
          TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
          TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido,
          ClaveSecreta = ConstantesCompartidasFacturacion.ClaveSecretaValida
        })
      };

      RespuestaConsultarClavesEmpresaExitosaSinClaveSecreta = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        ClavesEmpresa = JToken.FromObject(new
        {
          NumeroIdentificacion = ConstantesCompartidasFacturacion.NitExistente,
          TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
          TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido
        })
      };
    }
  }
}
