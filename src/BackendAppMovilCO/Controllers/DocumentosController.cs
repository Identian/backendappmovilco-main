using Aplicacion.Dto.Solicitudes;
using Aplicacion.Entidad.Solicitudes;
using Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendAppMovilCO.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiExplorerSettings(GroupName = "Documentos")]
  [Route("api/[controller]")]
  [ApiController]
  public class DocumentosController : ControllerBase
  {
    private readonly IDocumentosAplicacion _documentosAplicacion;
    private readonly IEmpresaAutenticacionAplicacion _empresaAutenticacionAplicacion;

    public DocumentosController(IDocumentosAplicacion documentosAplicacion, IEmpresaAutenticacionAplicacion empresaAutenticacionAplicacion)
    {
      _documentosAplicacion = documentosAplicacion;
      _empresaAutenticacionAplicacion = empresaAutenticacionAplicacion;
    }

    [HttpPost("Emitir")]
    public IActionResult EmisionDocumento([FromBody] SolicitudEmitirDocumentoDto solicitudEmitirDocumentoDto)
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString();
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion
      solicitudEmitirDocumentoDto.TipoApp ??= "1";
      var respuesta = _documentosAplicacion.EmitirDocumento(solicitudEmitirDocumentoDto, bearerToken, valorBearerToken);
      return Ok(respuesta);
    }

    [HttpPost("Consultar")]
    public IActionResult ConsultarDocumentos([FromBody] SolicitudConsultarDocumentosDto solicitudConsultarDocumentosDto)
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString();
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      var respuesta = _documentosAplicacion.ConsultarDocumentos(solicitudConsultarDocumentosDto, bearerToken, valorBearerToken);
      return Ok(respuesta);
    }

    [HttpGet("ConsultarEstado/{ConsecutivoDocumento}/{plataforma?}")]
    public IActionResult Consultar(string consecutivoDocumento, string plataforma = "TFHKA")
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      var respuestaEmpresaAutenticacion = _empresaAutenticacionAplicacion.ObtenerDatosToken(valorBearerToken);
      var solicitudDto = new SolicitudConsultarEstadoDocumentoFacturacionDto
      {
        IdEmpresa = respuestaEmpresaAutenticacion.IdEmpresa,
        Nit = respuestaEmpresaAutenticacion.NitEmpresa,
        TokenEmpresa = respuestaEmpresaAutenticacion.TokenEmpresa,
        TokenClave = respuestaEmpresaAutenticacion.TokenClave,
        Plataforma = plataforma,
        Consecutivo = consecutivoDocumento
      };
      var respuesta = _documentosAplicacion.ConsultarDocumentoPorConsecutivoFactura(solicitudDto, bearerToken, valorBearerToken);

      return Ok(respuesta);
    }

    [HttpPost("EnviarCorreo")]
    public IActionResult EnviarCorreoIndividual([FromBody] SolicitudEnviarCorreoIndividualDto solicitudDto)
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      var respuestaDto = _documentosAplicacion.EnviarCorreoIndividual(solicitudDto, bearerToken, valorBearerToken);
      return Ok(respuestaDto);
    }

    [HttpGet("ConsultarReferencia/{IdInvoice}/{TipoConsulta}/{plataforma?}")]
    public IActionResult ConsultarReferenciaDocumento(string IdInvoice, string TipoConsulta, string plataforma = "TFHKA")
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      var respuestaEmpresaAutenticacion = _empresaAutenticacionAplicacion.ObtenerDatosToken(valorBearerToken);
      var solicitudDto = new SolicitudConsultarReferenciaDocumentoFacturacionDto
      {
        IdEmpresa = respuestaEmpresaAutenticacion.IdEmpresa,
        Nit = respuestaEmpresaAutenticacion.NitEmpresa,
        TokenEmpresa = respuestaEmpresaAutenticacion.TokenEmpresa,
        TokenClave = respuestaEmpresaAutenticacion.TokenClave,
        Plataforma = plataforma,
        IdInvoice = IdInvoice,
        TipoConsulta = TipoConsulta
      };
      var respuesta = _documentosAplicacion.ConsultarReferenciaDocumentoFactura(solicitudDto, bearerToken, valorBearerToken);

      return Ok(respuesta);
    }

    [HttpGet("ConsultarMontoFacturaPos")]
    public IActionResult ConsultarMontoFacturaPos()
    {
      #region Token
      var bearerToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
      var valorBearerToken = HttpContext.User.FindFirst("context")!.Value;
      #endregion

      var respuesta = _documentosAplicacion.ConsultarMontoFacturaPos(bearerToken, valorBearerToken);
      return Ok(respuesta);
    }
  }
}
