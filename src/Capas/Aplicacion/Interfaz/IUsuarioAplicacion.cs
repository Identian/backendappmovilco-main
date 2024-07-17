using Aplicacion.Dto.Respuestas;

namespace Aplicacion.Interfaz
{
  public interface IUsuarioAplicacion
  {
    public RespuestaConsultarUsuarioFacturacionDto ConsultarInformacion(string bearerToken, string valorBearerToken);
  }
}
