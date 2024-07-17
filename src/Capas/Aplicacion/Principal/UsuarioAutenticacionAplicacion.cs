using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;
using Aplicacion.Interfaz;
using AutoMapper;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;

namespace Aplicacion.Principal
{
  public class UsuarioAutenticacionAplicacion : IUsuarioAutenticacionAplicacion
  {
    private readonly IUsuarioAutenticacionDominio _usuarioAutenticacionDominio;
    private readonly IMapper _mapeador;

    public UsuarioAutenticacionAplicacion(IUsuarioAutenticacionDominio usuarioAutenticacionDominio, IMapper mapeador)
    {
      _usuarioAutenticacionDominio = usuarioAutenticacionDominio;
      _mapeador = mapeador;
    }

    public RespuestaIniciarSesionDto AutenticarUsuario(UsuarioAutenticacionDto solicitudDto)
    {
      var respuestaDto = new RespuestaIniciarSesionDto();
      try
      {
        solicitudDto.AsignarValoresPorDefecto();
        #region Validaciones
        var validaciones = new UsuarioAutenticacionDtoValidator();
        var resultadoValidaciones = validaciones.Validate(solicitudDto);
        if (!resultadoValidaciones.IsValid)
        {
          respuestaDto.Codigo = 400;
          respuestaDto.response = "error";
          respuestaDto.message = resultadoValidaciones.Errors.FirstOrDefault().ErrorMessage;
          return respuestaDto;
        }
        #endregion
        var usuarioAutenticacion = _mapeador.Map<UsuarioAutenticacion>(solicitudDto);
        var respuesta = _usuarioAutenticacionDominio.AutenticarUsuario(usuarioAutenticacion);
        respuestaDto = _mapeador.Map<RespuestaIniciarSesionDto>(respuesta);
      }
      catch (Exception ex)
      {
        respuestaDto.Codigo = 500;
        respuestaDto.response = "error";
        respuestaDto.message = ex.Message;
      }
      return respuestaDto;
    }

    public RespuestaCerrarSesionDto CerrarSesion(string bearerToken)
    {
      var respuestaDto = new RespuestaCerrarSesionDto();
      try
      {
        var respuesta = _usuarioAutenticacionDominio.CerrarSesion(bearerToken);
        respuestaDto = _mapeador.Map<RespuestaCerrarSesionDto>(respuesta);
      }
      catch (Exception ex)
      {
        respuestaDto.Codigo = 500;
        respuestaDto.type = "error";
        respuestaDto.message = ex.Message;
      }
      return respuestaDto;
    }
  }
}