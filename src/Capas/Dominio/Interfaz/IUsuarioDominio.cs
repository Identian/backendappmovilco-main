using Dominio.Entidad.Respuestas;

namespace Dominio.Interfaz
{
  public interface IUsuarioDominio
  {
    public RespuestaConsultarUsuarioFacturacion ConsultarInformacion(string bearerToken, string valorBearerToken);
  }
}
