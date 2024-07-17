using Aplicacion.Dto.Solicitudes;
using Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendAppMovilCO.Controllers
{
  [ApiExplorerSettings(GroupName = "Sesión")]
  [Route("api/Sesion")]
  [ApiController]
  public class UsuarioAutenticacionController : ControllerBase
  {
    private readonly IUsuarioAutenticacionAplicacion _usuarioAutenticacionAplicacion;

    public UsuarioAutenticacionController(IUsuarioAutenticacionAplicacion usuarioAutenticacionAplicacion)
    {
      _usuarioAutenticacionAplicacion = usuarioAutenticacionAplicacion;
    }

    [HttpPost("Iniciar")]
    public IActionResult IniciarSesion([FromBody] UsuarioAutenticacionDto solicitudDto)
    {
      if (solicitudDto == null)
      {
        return (BadRequest());
      }
      var respuestaDto = _usuarioAutenticacionAplicacion.AutenticarUsuario(solicitudDto);
      return Ok(respuestaDto);
    }

    [HttpGet("Cerrar")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public IActionResult CerrarSesion()
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
      #endregion
      var respuestaDto = _usuarioAutenticacionAplicacion.CerrarSesion(bearerToken);
      return Ok(respuestaDto);
    }
  }
}
