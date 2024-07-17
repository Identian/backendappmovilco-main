using Aplicacion.Dto.Solicitudes;
using Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendAppMovilCO.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiExplorerSettings(GroupName = "Secuenciales")]
  [Route("api/[controller]")]
  [ApiController]
  public class SecuencialesController : Controller
  {
    private readonly ISecuencialesAplicacion _secuencialesAplicacion;
    private readonly IEmpresaAutenticacionAplicacion _empresaAutenticacionAplicacion;

    public SecuencialesController(ISecuencialesAplicacion secuencialesAplicacion, IEmpresaAutenticacionAplicacion empresaAutenticacionAplicacion)
    {
      _secuencialesAplicacion = secuencialesAplicacion;
      _empresaAutenticacionAplicacion = empresaAutenticacionAplicacion;
    }

    [HttpGet("Consultar/{plataforma?}")]
    public IActionResult ConsultarNumeraciones(string plataforma = "TFHKA")
    {
      #region Token
      var bearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      var respuestaEmpresaAutenticacion = _empresaAutenticacionAplicacion.ObtenerDatosToken(bearerToken);
      var solicitudDto = new SolicitudConsultarFacturacionDto
      {
        IdEmpresa = respuestaEmpresaAutenticacion.IdEmpresa,
        Nit = respuestaEmpresaAutenticacion.NitEmpresa,
        TokenEmpresa = respuestaEmpresaAutenticacion.TokenEmpresa,
        TokenClave = respuestaEmpresaAutenticacion.TokenClave,
        Plataforma = plataforma
      };
      var respuesta = _secuencialesAplicacion.ConsultarNumeraciones(solicitudDto);

      return Ok(respuesta);
    }

    [HttpPut("Seleccionar")]
    public IActionResult Seleccionar([FromBody] SolicitudSeleccionarSecuencialDto solicitud)
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      var respuestaDto = _secuencialesAplicacion.Seleccionar(solicitud, bearerToken, valorBearerToken);
      return (Ok(respuestaDto));
    }
  }
}
