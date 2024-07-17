using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;
using Aplicacion.Interfaz;
using AutoMapper;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;

namespace Aplicacion.Principal
{
  public class CertificadoAplicacion : ICertificadoAplicacion
  {
    private readonly ICertificadoDominio _certificadoDominio;
    private readonly IMapper _mapeador;

    public CertificadoAplicacion(ICertificadoDominio certificadoDominio, IMapper mapeador)
    {
      _certificadoDominio = certificadoDominio;
      _mapeador = mapeador;
    }

    public RespuestaConsultarCertificadoFacturacionDto Consultar(SolicitudConsultarFacturacionDto solicitudDto)
    {
      RespuestaConsultarCertificadoFacturacionDto respuestaDto = new();
      try
      {
        var validaciones = new SolicitudConsultarFacturacionDtoValidator();
        var resultadoValidaciones = validaciones.Validate(solicitudDto);
        if (resultadoValidaciones.IsValid)
        {
          var solicitud = _mapeador.Map<SolicitudConsultarFacturacion>(solicitudDto);
          var respuesta = _certificadoDominio.Consultar(solicitud);
          respuestaDto = _mapeador.Map<RespuestaConsultarCertificadoFacturacionDto>(respuesta);
        }
        else
        {
          respuestaDto.Codigo = 400;
          respuestaDto.Resultado = "Error";
          respuestaDto.Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones";
          var errores = new List<string>();
          foreach (var error in resultadoValidaciones.Errors)
          {
            errores.Add(error.ErrorMessage);
          }
          respuestaDto.Errores = errores;
        }
      }
      catch (Exception ex)
      {
        respuestaDto.Codigo = 500;
        respuestaDto.Mensaje = ex.Message;
        respuestaDto.Resultado = "Error";
      }
      return respuestaDto;
    }
  }
}
