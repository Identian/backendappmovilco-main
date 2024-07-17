using Aplicacion.Dto.Solicitudes;
using Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendAppMovilCO.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiExplorerSettings(GroupName = "Establecimientos")]
  [Route("api/[controller]")]
  [ApiController]
  public class EstablecimientosController : Controller
  {
    private readonly IEstablecimientosAplicacion _establecimientosAplicacion;

    public EstablecimientosController(IEstablecimientosAplicacion establecimientosAplicacion)
    {
      _establecimientosAplicacion = establecimientosAplicacion;
    }

    [HttpPut("Seleccionar")]
    public IActionResult Seleccionar([FromBody] SolicitudSeleccionarEstablecimientoDto solicitud)
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion


      var respuestaDto = _establecimientosAplicacion.Seleccionar(solicitud,  bearerToken, valorBearerToken);
      return (Ok(respuestaDto));
    }
  }
}
