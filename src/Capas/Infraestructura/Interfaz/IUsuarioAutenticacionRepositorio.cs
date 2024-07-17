using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Infraestructura.Interfaz
{
  public interface IUsuarioAutenticacionRepositorio
  {
    public RespuestaIniciarSesion AutenticarUsuario(UsuarioAutenticacion usuarioAutenticacion);
  }
}
