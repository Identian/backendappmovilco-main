using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;

namespace Aplicacion.Interfaz
{
  public interface IUsuarioAutenticacionAplicacion
  {
    public RespuestaIniciarSesionDto AutenticarUsuario(UsuarioAutenticacionDto solicitudDto);
    public RespuestaCerrarSesionDto CerrarSesion(string bearerToken);
  }
}
