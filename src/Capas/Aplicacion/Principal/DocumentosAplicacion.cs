using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;
using Aplicacion.Entidad.Respuestas;
using Aplicacion.Entidad.Solicitudes;
using Aplicacion.Interfaz;
using AutoMapper;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Microsoft.Extensions.Configuration;

namespace Aplicacion.Principal
{
  public class DocumentosAplicacion : IDocumentosAplicacion
  {
    private readonly IDocumentosDominio _documentosDominio;
    private readonly IMapper _mapeador;
    private readonly IConfiguration _configuracion;

    public DocumentosAplicacion(IConfiguration configuracion, IDocumentosDominio documentosDominio, IMapper mapeador)
    {
      _documentosDominio = documentosDominio;
      _mapeador = mapeador;
      _configuracion = configuracion;

    }

    public RespuestaEmitirDocumentoDto EmitirDocumento(SolicitudEmitirDocumentoDto solicitudEmitirDocumentoDto, string bearerToken, string valorBearerToken)
    {
      RespuestaEmitirDocumentoDto respuestaDto = new();
      try
      {
        var validaciones = new SolicitudEmitirDocumentoDtoValidator();
        var resultadoValidaciones = validaciones.Validate(solicitudEmitirDocumentoDto);
        if (resultadoValidaciones.IsValid)
        {
          var solicitudEmitirDocumento = _mapeador.Map<SolicitudEmitirDocumento>(solicitudEmitirDocumentoDto);
          var respuesta = _documentosDominio.EmitirDocumento(solicitudEmitirDocumento, bearerToken, valorBearerToken);
          respuestaDto = _mapeador.Map<RespuestaEmitirDocumentoDto>(respuesta);
        }
        else
        {
          respuestaDto.Codigo = 400;
          respuestaDto.Resultado = "Error";
          respuestaDto.Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones";
          var errores = new List<string>();
          foreach (var error in resultadoValidaciones.Errors)
          {
            errores.Add(error.ErrorMessage);
          }
          respuestaDto.MensajesValidacion = errores;
        }
      }
      catch (Exception ex)
      {
        respuestaDto.Codigo = 500;
        respuestaDto.Mensaje = ex.Message;
        respuestaDto.Resultado = "Error";
      }
      respuestaDto.FechaRespuesta = _documentosDominio.FormatoFechaHoraRespuestas(DateTime.UtcNow);
      return respuestaDto;
    }

    public RespuestaConsultarDocumentosDto ConsultarDocumentos(SolicitudConsultarDocumentosDto solicitudConsultarDocumentosDto, string bearerToken, string valorBearerToken)
    {
      RespuestaConsultarDocumentosDto respuestaDto = new();
      try
      {
        var validaciones = new SolicitudConsultarDocumentosDtoValidator();
        var resultadoValidaciones = validaciones.Validate(solicitudConsultarDocumentosDto);
        if (resultadoValidaciones.IsValid)
        {
          var solicitud = _mapeador.Map<SolicitudReporteEnLinea>(solicitudConsultarDocumentosDto);
          var respuesta = _documentosDominio.ConsultarDocumentos(solicitud, bearerToken, valorBearerToken);
          respuestaDto = _mapeador.Map<RespuestaConsultarDocumentosDto>(respuesta);
        }
        else
        {
          respuestaDto.Codigo = 400;
          respuestaDto.Resultado = "Error";
          respuestaDto.Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones";
          var errores = new List<string>();
          foreach (var error in resultadoValidaciones.Errors)
          {
            errores.Add(error.ErrorMessage);
          }
          respuestaDto.Errores = errores;
        }
      }
      catch (Exception ex)
      {
        respuestaDto.Codigo = 500;
        respuestaDto.Mensaje = ex.Message;
        respuestaDto.Resultado = "Error";
      }
      return respuestaDto;
    }

    public RespuestaConsultarEstadoDocumentoDto ConsultarDocumentoPorConsecutivoFactura(SolicitudConsultarEstadoDocumentoFacturacionDto solicitudConsultarDocumentosDto, string bearerToken, string valorBearerToken)
    {
      RespuestaConsultarEstadoDocumentoDto respuestaDto = new();
      try
      {
        var validaciones = new SolicitudConsultarEstadoDocumentoDtoValidator();
        var resultadoValidaciones = validaciones.Validate(solicitudConsultarDocumentosDto);
        if (resultadoValidaciones.IsValid)
        {
          var solicitud = _mapeador.Map<SolicitudConsultarEstadoDocumentoFacturacion>(solicitudConsultarDocumentosDto);
          var respuesta = _documentosDominio.ConsultarDocumentoPorConsecutivoFactura(solicitud, bearerToken, valorBearerToken);
          respuestaDto = _mapeador.Map<RespuestaConsultarEstadoDocumentoDto>(respuesta);
        }
        else
        {
          respuestaDto.Codigo = 400;
          respuestaDto.Resultado = "Error";
          respuestaDto.Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones";
          var errores = new List<string>();
          foreach (var error in resultadoValidaciones.Errors)
          {
            errores.Add(error.ErrorMessage);
          }
          respuestaDto.Errores = errores;
        }
      }
      catch (Exception ex)
      {
        respuestaDto.Codigo = 500;
        respuestaDto.Mensaje = ex.Message;
        respuestaDto.Resultado = "Error";
      }
      return respuestaDto;
    }

    public RespuestaEnviarCorreoIndividualDto EnviarCorreoIndividual(SolicitudEnviarCorreoIndividualDto solicitudDto, string bearerToken, string valorBearerToken)
    {
      RespuestaEnviarCorreoIndividualDto respuestaDto = new();
      try
      {
        var validaciones = new SolicitudEnviarCorreoIndividualDtoValidator();
        var resultadoValidaciones = validaciones.Validate(solicitudDto);
        if (resultadoValidaciones.IsValid)
        {
          var solicitud = _mapeador.Map<SolicitudEnviarCorreoIndividual>(solicitudDto);
          var respuesta = _documentosDominio.EnviarCorreoIndividual(solicitud, bearerToken, valorBearerToken);
          respuestaDto = _mapeador.Map<RespuestaEnviarCorreoIndividualDto>(respuesta);
        }
        else
        {
          respuestaDto.Codigo = 400;
          respuestaDto.Resultado = "Error";
          respuestaDto.Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones";
          var errores = new List<string>();
          foreach (var error in resultadoValidaciones.Errors)
          {
            errores.Add(error.ErrorMessage);
          }
          respuestaDto.Errores = errores;
        }
      }
      catch (Exception ex)
      {
        respuestaDto.Codigo = 500;
        respuestaDto.Mensaje = ex.Message;
        respuestaDto.Resultado = "Error";
      }
      return (respuestaDto);
    }

    public RespuestaConsultarReferenciaDocumentoDto ConsultarReferenciaDocumentoFactura(SolicitudConsultarReferenciaDocumentoFacturacionDto solicitudConsultarDocumentosDto, string bearerToken, string valorBearerToken)
    {
      RespuestaConsultarReferenciaDocumentoDto respuestaDto = new();
      try
      {
        var validaciones = new SolicitudConsultarReferenciaDocumentoDtoValidator(_configuracion);
        var resultadoValidaciones = validaciones.Validate(solicitudConsultarDocumentosDto);
        if (resultadoValidaciones.IsValid)
        {
          var solicitud = _mapeador.Map<SolicitudConsultarReferenciaDocumentoFacturacion>(solicitudConsultarDocumentosDto);
          var respuesta = _documentosDominio.ConsultarReferenciaDocumentosFactura(solicitud, bearerToken, valorBearerToken);
          respuestaDto = _mapeador.Map<RespuestaConsultarReferenciaDocumentoDto>(respuesta);
        }
        else
        {
          respuestaDto.Codigo = 400;
          respuestaDto.Resultado = "Error";
          respuestaDto.Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones";
          var errores = new List<string>();
          foreach (var error in resultadoValidaciones.Errors)
          {
            errores.Add(error.ErrorMessage);
          }
          respuestaDto.Errores = errores;
        }
      }
      catch (Exception ex)
      {
        respuestaDto.Codigo = 500;
        respuestaDto.Mensaje = ex.Message;
        respuestaDto.Resultado = "Error";
      }
      return respuestaDto;
    }

    public RespuestaConsultarMontoFacturaPosDto ConsultarMontoFacturaPos(string bearerToken, string valorBearerToken)
    {
      RespuestaConsultarMontoFacturaPosDto respuestaDto = new();
      try
      {
        var respuesta = _documentosDominio.ConsultarMontoFacturaPos(bearerToken, valorBearerToken);
        respuestaDto = _mapeador.Map<RespuestaConsultarMontoFacturaPosDto>(respuesta);
      }
      catch (Exception ex)
      {
        respuestaDto.Codigo = 500;
        respuestaDto.Mensaje = ex.Message;
        respuestaDto.Resultado = "Error";
      }
      return respuestaDto;
    }
  }
}
