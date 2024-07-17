using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Dominio.Interfaz
{
  public interface IUsuarioAutenticacionDominio
  {
    public RespuestaIniciarSesion AutenticarUsuario(UsuarioAutenticacion solicitud);
    public RespuestaCerrarSesion CerrarSesion(string bearerToken);
  }
}
