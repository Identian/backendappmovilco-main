using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;
using Aplicacion.Interfaz;
using AutoMapper;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Newtonsoft.Json.Linq;
using Transversal.Comun.Respuestas;

namespace Aplicacion.Principal
{
  public class EstablecimientosAplicacion : IEstablecimientosAplicacion
  {
    public readonly IEstablecimientosDominio _establecimientosDominio;
    public readonly IMapper _mapeador;

    public EstablecimientosAplicacion(IEstablecimientosDominio establecimientosDominio, IMapper mapeador)
    {
      _establecimientosDominio = establecimientosDominio;
      _mapeador = mapeador;
    }

    public RespuestaSeleccionarEstablecimientoDto Seleccionar(SolicitudSeleccionarEstablecimientoDto solicitudDto, string bearerToken, string valorBearerToken)
    {
      var respuestaDto = new RespuestaSeleccionarEstablecimientoDto();
      var validaciones = new SolicitudSeleccionarEstablecimientoDtoValidator();
      var resultadoValidaciones = validaciones.Validate(solicitudDto);
      if (resultadoValidaciones.IsValid)
      {
        try
        {
          var solicitud = _mapeador.Map<SolicitudSeleccionarEstablecimiento>(solicitudDto);
          var respuesta = _establecimientosDominio.Seleccionar(solicitud, bearerToken ,valorBearerToken);
          respuestaDto = _mapeador.Map<RespuestaSeleccionarEstablecimientoDto>(respuesta);
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
