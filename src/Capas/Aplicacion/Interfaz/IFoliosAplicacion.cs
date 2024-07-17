using Aplicacion.Dto.Respuestas;

namespace Aplicacion.Interfaz
{
  public interface IFoliosAplicacion
  {
    public RespuestaConsultarResumenFoliosDto ConsultarResumen(string bearerToken);
  }
}
