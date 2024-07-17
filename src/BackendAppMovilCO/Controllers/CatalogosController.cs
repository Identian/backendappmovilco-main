using Aplicacion.Dto.Solicitudes;
using Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendAppMovilCO.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiExplorerSettings(GroupName = "Catálogos")]
  [Route("api/Catalogos")]
  [ApiController]
  public class CatalogosController : Controller
  {
    private readonly ICatalogosAplicacion _catalogosAplicacion;

    public CatalogosController(ICatalogosAplicacion catalogosAplicacion)
    {
      _catalogosAplicacion = catalogosAplicacion;
    }

    [HttpGet("Consultar/{identificador}")]
    [HttpGet("Consultar")]
    public IActionResult Consultar(string? identificador = null)
    {
      SolicitudConsultarCatalogoDto solicitudDto = new()
      {
        Identificador = identificador
      };
      var respuestaDto = _catalogosAplicacion.Consultar(solicitudDto);
      return Ok(respuestaDto);
    }
  }
}
