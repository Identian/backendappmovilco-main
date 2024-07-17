using Dominio.Entidad.Respuestas;

namespace Infraestructura.Interfaz
{
  public interface IRedisCacheRepositorio
  {
    public RespuestaIniciarSesion ConsultarUsuarioAutenticado(RespuestaIniciarSesion inicioSesion);
    public RespuestaIniciarSesion EliminarUsuarioAutenticado(RespuestaIniciarSesion inicioSesion);
    public RespuestaIniciarSesion InsertarUsuarioAutenticado(RespuestaIniciarSesion inicioSesion);
    public RespuestaIniciarSesion ActualizarUsuarioAutenticado(RespuestaIniciarSesion inicioSesion);
  }
}
