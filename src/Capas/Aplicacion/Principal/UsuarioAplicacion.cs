using Aplicacion.Dto.Respuestas;
using Aplicacion.Interfaz;
using AutoMapper;
using Dominio.Interfaz;

namespace Aplicacion.Principal
{
  public class UsuarioAplicacion : IUsuarioAplicacion
  {
    private readonly IUsuarioDominio _usuarioDominio;
    private readonly IMapper _mapeador;

    public UsuarioAplicacion(IUsuarioDominio usuarioDominio, IMapper mapeador)
    {
      _usuarioDominio = usuarioDominio;
      _mapeador = mapeador;
    }

    public RespuestaConsultarUsuarioFacturacionDto ConsultarInformacion(string bearerToken, string valorBearerToken)
    {
      var respuestaDto = new RespuestaConsultarUsuarioFacturacionDto();
      try
      {
        var respuesta = _usuarioDominio.ConsultarInformacion(bearerToken, valorBearerToken);
        respuestaDto = _mapeador.Map<RespuestaConsultarUsuarioFacturacionDto>(respuesta);
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
