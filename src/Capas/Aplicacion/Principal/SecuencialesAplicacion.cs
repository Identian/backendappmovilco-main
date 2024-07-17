using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;
using Aplicacion.Interfaz;
using AutoMapper;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;

namespace Aplicacion.Principal
{
  public class SecuencialesAplicacion : ISecuencialesAplicacion
  {
    private readonly ISecuencialesDominio _secuencialesDominio;
    private readonly IMapper _mapeador;
    public SecuencialesAplicacion(ISecuencialesDominio secuencialesDominio, IMapper mapeador)
    {
      _secuencialesDominio = secuencialesDominio;
      _mapeador = mapeador;
    }

    public RespuestaConsultarNumeracionesDto ConsultarNumeraciones(SolicitudConsultarFacturacionDto solicitudConsultarNumeracionesDto)
    {
      RespuestaConsultarNumeracionesDto respuestaDto = new();
      try
      {
        var validaciones = new SolicitudConsultarFacturacionDtoValidator();
        var resultadoValidaciones = validaciones.Validate(solicitudConsultarNumeracionesDto);
        if (resultadoValidaciones.IsValid)
        {
          var solicitudConsultarNumeraciones = _mapeador.Map<SolicitudConsultarFacturacion>(solicitudConsultarNumeracionesDto);
          var respuesta = _secuencialesDominio.ConsultarNumeraciones(solicitudConsultarNumeraciones);
          respuestaDto = _mapeador.Map<RespuestaConsultarNumeracionesDto>(respuesta);
        }
        else
        {
          respuestaDto.Codigo = 400;
          respuestaDto.Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones";
          respuestaDto.Resultado = "Error";
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

    public RespuestaSeleccionarSecuencialDto Seleccionar(SolicitudSeleccionarSecuencialDto solicitudDto, string bearerToken, string valorBearerToken)
    {
      var respuestaDto = new RespuestaSeleccionarSecuencialDto();
      var validaciones = new SolicitudSeleccionarSecuencialDtoValidator();
      var resultadoValidaciones = validaciones.Validate(solicitudDto);
      if (resultadoValidaciones.IsValid)
      {
        try
        {
          var solicitud = _mapeador.Map<SolicitudSeleccionarSecuencial>(solicitudDto);
          var respuesta = _secuencialesDominio.Seleccionar(solicitud, bearerToken, valorBearerToken);
          respuestaDto = _mapeador.Map<RespuestaSeleccionarSecuencialDto>(respuesta);
        }
        catch (Exception ex)
        {
          respuestaDto.Codigo = 500;
          respuestaDto.Mensaje = ex.Message;
          respuestaDto.Resultado = "Error";
        }
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
      return respuestaDto;
    }
  }
}
