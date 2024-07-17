using Aplicacion.Dto.Solicitudes.FiltrosSolicitudes;
using Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendAppMovilCO.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiExplorerSettings(GroupName = "Indicadores")]
  [Route("api/[controller]")]
  [ApiController]
  public class IndicadoresController : Controller
  {
    private readonly IIndicadoresAplicacion _indicadoresAplicacion;

    public IndicadoresController(IIndicadoresAplicacion indicadoresAplicacion)
    {
      _indicadoresAplicacion = indicadoresAplicacion;
    }

    [HttpPost("Documentos/Totales")]
    public IActionResult TotalDocumentos([FromBody] FiltrosTotalDocumentosDto filtrosDto)
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      if (filtrosDto == null)
      {
        return (BadRequest());
      }
      var respuestaDto = _indicadoresAplicacion.ConsultarTotalDocumentos(filtrosDto, bearerToken, valorBearerToken);
      return (Ok(respuestaDto));
    }
  }
}
