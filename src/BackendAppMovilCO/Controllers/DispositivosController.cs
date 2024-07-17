using Aplicacion.Dto.Dispositivos;
using Aplicacion.Dto.Solicitudes.Dispositivos;
using Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BackendAppMovilCO.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiExplorerSettings(GroupName = "Dispositivos")]
  [Route("api/Dispositivos")]
  [ApiController]
  public class DispositivosController : Controller
  {
    private readonly IDispositivosAplicacion _dispositivosAplicacion;

    public DispositivosController(IDispositivosAplicacion dispositivosAplicacion)
    {
      _dispositivosAplicacion = dispositivosAplicacion;
    }

    [HttpPost("Crear")]
    public IActionResult Crear([FromBody] DispositivoDto solicitudDto)
    {

      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString();
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      JToken? respuestaDto = _dispositivosAplicacion.CrearAccesoDispositivo(solicitudDto, bearerToken, valorBearerToken);
      return Ok(respuestaDto);
    }

    [HttpGet("Suscripcion/Consultar/{serialLogico}")]
    public IActionResult ConsultarSuscripcion(string serialLogico)
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString();
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      SolicitudConsultarSuscripcionDispositivoDto solicitudDto = new()
      {
        SerialLogico = serialLogico
      };

      JToken? respuestaDto = _dispositivosAplicacion.ConsultarSuscripcionDispositivo(solicitudDto, bearerToken, valorBearerToken);
      return Ok(respuestaDto);
    }

    [HttpPut("AsociarAlias")]
    public IActionResult AsociarAlias([FromBody] SolicitudAsociarAliasDto solicitudDto)
    {

      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString();
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      JToken? respuestaDto = _dispositivosAplicacion.AsociarAlias(solicitudDto, bearerToken, valorBearerToken);
      return Ok(respuestaDto);
    }
  }
}
