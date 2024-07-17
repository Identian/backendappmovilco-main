using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;
using Aplicacion.Interfaz;
using AutoMapper;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;

namespace Aplicacion.Principal
{
  public class FoliosAplicacion: IFoliosAplicacion
  {
    private readonly IFoliosDominio _foliosDominio;
    private readonly IMapper _mapeador;

    public FoliosAplicacion(IFoliosDominio foliosDominio, IMapper mapeador) { 
      _foliosDominio= foliosDominio;
      _mapeador= mapeador;

    }
    public RespuestaConsultarResumenFoliosDto ConsultarResumen(string bearerToken)
    {
      RespuestaConsultarResumenFoliosDto respuestaDto = new();
      try {
        var respuesta = _foliosDominio.ConsultarResumen(bearerToken);
        respuestaDto = _mapeador.Map<RespuestaConsultarResumenFoliosDto>(respuesta);
        
      }catch(Exception ex)
      {
        respuestaDto.Codigo = 500;
        respuestaDto.Mensaje = ex.Message;
        respuestaDto.Resultado = "Error";
      }
      return respuestaDto;
    }

   
  }
}
