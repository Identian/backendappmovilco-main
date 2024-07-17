using Dominio.Entidad.Respuestas;
using Dominio.Interfaz;
using Infraestructura.Interfaz;

namespace Dominio.Core
{
  public class UsuarioDominio : IUsuarioDominio
  {
    private readonly IRedisCacheRepositorio _redisCacheRepositorio;
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly IEmpresaAutenticacionRepositorio _empresaAutenticacionRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public UsuarioDominio(IRedisCacheRepositorio redisCacheRepositorio, IUsuarioRepositorio usuarioRepositorio, IEmpresaAutenticacionRepositorio empresaAutenticacionRepositorio, IEmpresaRepositorio empresaRepositorio)
    {
      _redisCacheRepositorio = redisCacheRepositorio;
      _usuarioRepositorio = usuarioRepositorio;
      _empresaAutenticacionRepositorio = empresaAutenticacionRepositorio;
      _empresaRepositorio = empresaRepositorio;
    }

    public RespuestaConsultarUsuarioFacturacion ConsultarInformacion(string bearerToken, string valorBearerToken)
    {
      var respuesta = new RespuestaConsultarUsuarioFacturacion();
      var respuestaRollUsuario = new RespuestaConsultarRollUsuario();
      //Obtener correo de usuario en caché
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken
      };
      var usuarioEnCache = _redisCacheRepositorio.ConsultarUsuarioAutenticado(inicioSesion);
      if ((usuarioEnCache != null) && (usuarioEnCache.Codigo == 200))
      {
        var respuestaToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
        respuesta = _usuarioRepositorio.ConsultarInformacion(respuestaToken.IdEmpresa, respuestaToken.NitEmpresa!, usuarioEnCache.Usuario!.Correo!);
        if (respuesta.Codigo == 200)
        {
          respuestaRollUsuario = _usuarioRepositorio.ConsultarRollUsuario(respuesta.Usuario!.Id!);
          respuesta.Usuario.Roles = respuestaRollUsuario.rollUsuario;
        }
      }
      else
      {
        respuesta.Codigo = 401;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "Se ha cerrado la sesión del usuario";
      }
      return respuesta;
    }

  }
}
