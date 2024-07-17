using Dominio.Entidad.Respuestas;
using Newtonsoft.Json.Linq;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasDispositivos
  {
    public JToken? DatosValidosCrearDispositivo;
    public JToken? DatosValidosAsociarAlias;
    public JToken? DatosInvalidosAsociarAlias;
    public JToken? DatosInvalidosDispositivoYaExiste;

    public RespuestaValidadSubscripcion SuscripcionDispositivoExistente = new();
    public RespuestaValidadSubscripcion SuscripcionDispositivoNoActiva = new();
    public RespuestaValidadSubscripcion SuscripcionDispositivoNoVigente = new();
    public RespuestaValidadSubscripcion SuscripcionDispositivoAunNoVigente = new();
    public RespuestaValidadSubscripcion SuscripcionDispositivoInexistente = new();
    public RespuestaValidadSubscripcion SuscripcionDispositivoNoAsociadoAEmpresa = new();

    public JToken? ConsultarSuscripcionDispositivoPorSerialLogicoExistente;
    public JToken? ConsultarSuscripcionDispositivoPorSerialLogicoInexistente;
    public JToken? ConsultarSuscripcionDispositivoPorSerialLogicoNoRegistrado;

    public JToken? ConsultarSuscripcionDispositivoExistente;
    public JToken? ConsultarSuscripcionDispositivoInexistente;
    public JToken? ConsultarSuscripcionDispositivoNoRegistrado;
    public JToken? RespuestaDatosTokenInvalidos;

    public void InicializarRespuestas()
    {
      DatosValidosCrearDispositivo = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDispositivoNuevo,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDispositivoNuevo,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDispositivoNuevo,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        IdDispositivo = ConstantesCompartidasFacturacion.IdDispositivoNuevo,
        ActivoApp = ConstantesCompartidasFacturacion.ActivoAppDispositivoNuevo,
      });

      DatosInvalidosDispositivoYaExiste = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosInvalidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosInvalidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosInvalidosDispositivoExiste,
      });

      RespuestaDatosTokenInvalidos = JToken.FromObject(new
      {
        Codigo = 401,
        Resultado = "Error",
        Mensaje = "No autorizado. Capa de Dominio",
        Errores = new string[] {
          "Datos Token inválidos"
        }
      });


      #region Asociar Alias
      DatosValidosAsociarAlias = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidosActualizacion,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        IdDispositivo = ConstantesCompartidasFacturacion.IdDispositivoNuevo,
      });

      DatosInvalidosAsociarAlias = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoNoEncontrado,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosInvalidosAsociarAlias,
      });
      #endregion

      #region Validar Suscripcion Dispositivo
      SuscripcionDispositivoExistente = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoExistente,
        IdSuscripcion = ConstantesCompartidasFacturacion.IdSuscripcionDispositivoExistente
      };

      SuscripcionDispositivoNoActiva = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoErrorValidacionSuscripcion,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoNoActiva
      };

      SuscripcionDispositivoNoVigente = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoErrorValidacionSuscripcion,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoNoVigente
      };

      SuscripcionDispositivoAunNoVigente = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoErrorValidacionSuscripcion,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoAunNoVigente
      };

      SuscripcionDispositivoInexistente = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoErrorValidacionSuscripcion,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoInexistente
      };

      SuscripcionDispositivoNoAsociadoAEmpresa = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoErrorValidacionSuscripcion,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoNoAsociadoAEmpresa
      };
      #endregion

      #region Consultar Suscripcion Dispositivo Por Serial Logico
      ConsultarSuscripcionDispositivoPorSerialLogicoExistente = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        IdSuscripcion = Convert.ToInt32(ConstantesCompartidasFacturacion.IdSuscripcionDispositivoExistente),
        SerialSuscripcion = ConstantesCompartidasFacturacion.SerialSuscripcionDispositivoExistente
      });

      ConsultarSuscripcionDispositivoPorSerialLogicoInexistente = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoNoEncontrado,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoInexistente,
        IdSuscripcion = (int?)null,
        SerialSuscripcion = (string?)null
      });

      ConsultarSuscripcionDispositivoPorSerialLogicoNoRegistrado = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoNoEncontrado,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeRegistroNoEncontradoDispositivoSerialLogicoInexistente,
        IdSuscripcion = (int?)null,
        SerialSuscripcion = (string?)null
      });
      #endregion

      #region Consultar Suscripcion Dispositivo
      ConsultarSuscripcionDispositivoExistente = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Suscripciones = new List<JToken>
        {
          JToken.FromObject(new
          {
            Id = ConstantesCompartidasFacturacion.IdSuscripcionDispositivoExistente,
            Serial = ConstantesCompartidasFacturacion.SerialSuscripcionDispositivoExistente,
            FechaInicio = "2024-01-01 00:00:00",
            FechaFin = "2030-01-01 00:00:00",
            Activo = "1",
            Dispositivo = JToken.FromObject(new
            {
              Id = ConstantesCompartidasFacturacion.IdDispositivoSuscripcionExistente,
              Serial = ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionExistente,
              SerialAntiguo = (string?)null,
              SerialLogioco = ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionExistente,
              Nombre = "Dispositivo Prueba",
              Marca = "Marca Prueba",
              Modelo = "Modelo Prueba",
              Tipo = "Tipo Prueba",
              TipoApp = ConstantesCompartidasFacturacion.IdTipoAppTili,
              NombreTipoApp = ConstantesCompartidasFacturacion.NombreTipoAppTili,
              Version = (string?)null,
              ActivoApp = "1",
              Bloqueado = "0"
            })
          })
        },
        Errores = (string?)null
      });

      ConsultarSuscripcionDispositivoInexistente = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoNoEncontrado,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoInexistente,
        Nit = (string?)null,
        IdEmpresa = (string?)null,
        Suscripciones = (string?)null,
        Errores = (string?)null
      });

      ConsultarSuscripcionDispositivoNoRegistrado = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoNoEncontrado,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeRegistroNoEncontradoDispositivoSerialLogicoInexistente,
        Nit = (string?)null,
        IdEmpresa = (string?)null,
        Suscripciones = (string?)null,
        Errores = (string?)null
      });
      #endregion
    }

    public RespuestasCompartidasDispositivos()
    {
      InicializarRespuestas();
    }
  }
}
