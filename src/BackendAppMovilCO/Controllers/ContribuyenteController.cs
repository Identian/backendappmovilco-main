using Aplicacion.Dto.Solicitudes;
using Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendAppMovilCO.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiExplorerSettings(GroupName = "Contribuyente")]
  [Route("api/Contribuyente")]
  [ApiController]
  public class ContribuyenteController : Controller
  {
    private readonly IEmpresaAplicacion _empresaAplicacion;
    private readonly IEmpresaAutenticacionAplicacion _empresaAutenticacionAplicacion;
    private readonly IFoliosAplicacion _foliosAplicacion;
    private readonly ICertificadoAplicacion _certificadoAplicacion;

    public ContribuyenteController(IEmpresaAplicacion empresaAplicacion, IEmpresaAutenticacionAplicacion empresaAutenticacionAplicacion, IFoliosAplicacion foliosAplicacion, ICertificadoAplicacion certificadoAplicacion)
    {
      _empresaAplicacion = empresaAplicacion;
      _empresaAutenticacionAplicacion = empresaAutenticacionAplicacion;
      _foliosAplicacion = foliosAplicacion;
      _certificadoAplicacion = certificadoAplicacion;
    }

    [HttpGet("InformacionFiscal/{plataforma?}")]
    public IActionResult Consultar(string plataforma = "TFHKA")
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      var solicitudDto = new SolicitudConsultarFacturacionDto
      {
        Plataforma = plataforma
      };
      var respuesta = _empresaAplicacion.ConsultarEmpresa(solicitudDto, bearerToken, valorBearerToken);

      return Ok(respuesta);
    }

    [HttpGet("Folios")]
    public IActionResult ConsultarResumenFolios()
    {
      var bearerToken = HttpContext.User.FindFirst("context")!.Value;
      var respuesta = _foliosAplicacion.ConsultarResumen(bearerToken);
      return Ok(respuesta);
    }

    [HttpGet("ConsultarCertificado")]
    public IActionResult ConsultarCertificado()
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
        TokenClave = respuestaEmpresaAutenticacion.TokenClave
      };

      var respuesta = _certificadoAplicacion.Consultar(solicitudDto);
      return Ok(respuesta);
    }

    [HttpPost("ValidarClaveSecreta")]
    public IActionResult ValidarClaveSecreta([FromBody] SolicitudValidarClaveSecretaDto solicitudDto)
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      var respuesta = _empresaAutenticacionAplicacion.validarClaveSecreta(solicitudDto, bearerToken, valorBearerToken);
      return Ok(respuesta);
    }
  }
}
