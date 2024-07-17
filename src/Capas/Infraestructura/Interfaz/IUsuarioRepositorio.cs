using Dominio.Entidad.Respuestas;

namespace Infraestructura.Interfaz
{
  public interface IUsuarioRepositorio
  {
    public RespuestaConsultarUsuarioFacturacion ConsultarInformacion(string idEmpresa, string nitEmpresa, string correoUsuario);
    public RespuestaConsultarRollUsuario ConsultarRollUsuario(string idUsuario);
  }
}
