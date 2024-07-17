using Aplicacion.Dto.Dispositivos;
using Aplicacion.Dto.Solicitudes.Dispositivos;
using Aplicacion.Dto.Solicitudes.Validaciones;
using Aplicacion.Dto.Solicitudes.Validaciones.Dispositivos;
using Aplicacion.Interfaz;
using AutoMapper;
using Dominio.Entidad.Dispositivos;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Newtonsoft.Json.Linq;
using Transversal.Comun.Respuestas;

namespace Aplicacion.Principal
{
  public class DispositivosAplicacion : IDispositivosAplicacion
  {
    public readonly IDispositivosDominio _dispositivosDominio;
    public readonly IMapper _mapeador;

    public DispositivosAplicacion(IDispositivosDominio dispositivosDominio, IMapper mapeador)
    {
      _dispositivosDominio = dispositivosDominio;
      _mapeador = mapeador;
    }

    public JToken? CrearAccesoDispositivo(DispositivoDto solicitudDto, string bearerToken, string valorBearerToken)
    {
      JToken respuestaDto;
      try
      {
        #region Validaciones
        var validaciones = new DispositivoDtoValidator();
        var resultadoValidaciones = validaciones.Validate(solicitudDto);
        if (!resultadoValidaciones.IsValid)
        {
          var errores = new List<string>();
          foreach (var error in resultadoValidaciones.Errors)
          {
            errores.Add(error.ErrorMessage);
          }
          RespuestaBase respuestaDtoConErrores = new()
          {
            Codigo = 400,
            Resultado = "Error",
            Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones",
            Errores = errores
          };
          return (JToken.FromObject(respuestaDtoConErrores));
        }
        #endregion Validaciones
        var solicitud = _mapeador.Map<Dispositivo>(solicitudDto);
        respuestaDto = _dispositivosDominio.CrearAccesoDispositivo(solicitud, bearerToken, valorBearerToken)!;
      }
      catch (Exception ex)
      {
        respuestaDto = JToken.FromObject(RespuestaBase.Error500("Aplicación", ex.Message));
      }
      return (respuestaDto);
    }

    public JToken? ConsultarSuscripcionDispositivo(SolicitudConsultarSuscripcionDispositivoDto solicitudDto, string bearerToken, string valorBearerToken)
    {
      JToken respuestaDto;
      try
      {
        #region Validaciones
        var validaciones = new SolicitudConsultarSuscripcionDispositivoDtoValidator();
        var resultadoValidaciones = validaciones.Validate(solicitudDto);
        if (!resultadoValidaciones.IsValid)
        {
          var errores = new List<string>();
          foreach (var error in resultadoValidaciones.Errors)
          {
            errores.Add(error.ErrorMessage);
          }
          RespuestaBase respuestaDtoConErrores = new()
          {
            Codigo = 400,
            Resultado = "Error",
            Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones",
            Errores = errores
          };
          return (JToken.FromObject(respuestaDtoConErrores));
        }
        #endregion Validaciones
        respuestaDto = _dispositivosDominio.ConsultarSuscripcionDispositivo(solicitudDto.SerialLogico!, bearerToken, valorBearerToken)!;
      }
      catch (Exception ex)
      {
        respuestaDto = JToken.FromObject(RespuestaBase.Error500("Aplicación", ex.Message));
      }
      return (respuestaDto);
    }

    public JToken? AsociarAlias(SolicitudAsociarAliasDto solicitudDto, string bearerToken, string valorBearerTokenn)
    {
      JToken respuestaDto;
      try
      {
        #region Validaciones
        var validaciones = new SolicitudAsociarAliasDtoValidator();
        var resultadoValidaciones = validaciones.Validate(solicitudDto);
        if (!resultadoValidaciones.IsValid)
        {
          var errores = new List<string>();
          foreach (var error in resultadoValidaciones.Errors)
          {
            errores.Add(error.ErrorMessage);
          }
          RespuestaBase respuestaDtoConErrores = new()
          {
            Codigo = 400,
            Resultado = "Error",
            Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones",
            Errores = errores
          };
          return (JToken.FromObject(respuestaDtoConErrores));
        }
        #endregion Validaciones
        var solicitud = _mapeador.Map<SolicitudAsociarAlias>(solicitudDto);
        respuestaDto = _dispositivosDominio.AsociarAlias(solicitud!, bearerToken, valorBearerTokenn)!;
      }
      catch (Exception ex)
      {
        respuestaDto = JToken.FromObject(RespuestaBase.Error500("Aplicación", ex.Message));
      }
      return (respuestaDto);

    }
  }
}
