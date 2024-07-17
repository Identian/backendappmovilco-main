using Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendAppMovilCO.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiExplorerSettings(GroupName = "Usuario")]
  [Route("api/[controller]")]
  [ApiController]
  public class UsuarioController : ControllerBase
  {
    private readonly IUsuarioAplicacion _usuarioAplicacion;

    public UsuarioController(IUsuarioAplicacion usuarioAplicacion)
    {
      _usuarioAplicacion = usuarioAplicacion;
    }

    [HttpGet("Informacion")]
    public IActionResult ConsultarInformacion()
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion
      var respuestaDto = _usuarioAplicacion.ConsultarInformacion(bearerToken, valorBearerToken);
      return Ok(respuestaDto);
    }
  }
}
