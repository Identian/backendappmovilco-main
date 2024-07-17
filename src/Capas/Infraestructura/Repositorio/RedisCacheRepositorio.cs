using Dominio.Entidad.Respuestas;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Transversal.Comun.Fabricas;
using Transversal.Comun.Utils;

namespace Infraestructura.Repositorio
{
  public class RedisCacheRepositorio : IRedisCacheRepositorio
  {
    private readonly IConfiguration _configuracion;
    private readonly IFabricaConexionRedis _fabricaConexionRedis;

    public RedisCacheRepositorio(IConfiguration configuracion, IFabricaConexionRedis fabricaConexionRedis)
    {
      _configuracion = configuracion;
      _fabricaConexionRedis = fabricaConexionRedis;
    }

    private string PrepararLlaveParaCache(RespuestaIniciarSesion inicioSesion)
    {
      return
      (
        _configuracion["RedisCache:FormatoLlave:IniciarSesion"]!
          .Replace("{ambiente}", _configuracion["Ambiente"])
          .Replace("{bearerToken}", inicioSesion.token)
      );
    }

    public RespuestaIniciarSesion ConsultarUsuarioAutenticado(RespuestaIniciarSesion inicioSesion)
    {
      if (!_fabricaConexionRedis.Cache.TryGetValue(PrepararLlaveParaCache(inicioSesion), out inicioSesion, out string errorMessageGet))
      {
        inicioSesion = new()
        {
          Codigo = 404,
          response = "Error",
          message = "No se encontró la información sobre el inicio de sesión buscado en la Redis Caché",
          Errores = new() { errorMessageGet }
        };
        if (errorMessageGet == UtilidadesCadenas.TimeoutRedisCache)
        {
          inicioSesion = new()
          {
            Codigo = 504,
            response = "Error",
            message = "Se supero el tiempo de conexion a la redis cache",
            Errores = new() { errorMessageGet }
          };
        }
      }
      return (inicioSesion);
    }

    public RespuestaIniciarSesion EliminarUsuarioAutenticado(RespuestaIniciarSesion inicioSesion)
    {
      if (!_fabricaConexionRedis.Cache.Remove(PrepararLlaveParaCache(inicioSesion), out string errorMessageGet))
      {
        inicioSesion.Codigo = 404;
        inicioSesion.response = "Error";
        inicioSesion.message = "No se pudo eliminar la información sobre el inicio de sesión en la Redis Caché";
        inicioSesion.Errores = new() { errorMessageGet };
      }
      return (inicioSesion);
    }

    public RespuestaIniciarSesion InsertarUsuarioAutenticado(RespuestaIniciarSesion inicioSesion)
    {
      if (!_fabricaConexionRedis.Cache.Set(PrepararLlaveParaCache(inicioSesion), inicioSesion, out string errorMessageGet, inicioSesion.TiempoExpiracion))
      {
        inicioSesion.Codigo = 200;
        inicioSesion.response = "Procesado con errores";
        inicioSesion.message = "No se pudo insertar la información sobre el inicio de sesión en la Redis Caché";
        inicioSesion.Errores = new() { errorMessageGet };
      }
      return (inicioSesion);
    }

    public RespuestaIniciarSesion ActualizarUsuarioAutenticado(RespuestaIniciarSesion inicioSesion)
    {
      if (!_fabricaConexionRedis.Cache.Update(PrepararLlaveParaCache(inicioSesion), inicioSesion, out string errorMessageGet, inicioSesion.TiempoExpiracion))
      {
        inicioSesion.Codigo = 404;
        inicioSesion.response = "Error";
        inicioSesion.message = "No se pudo actualizar la información sobre el inicio de sesión en la Redis Caché";
        inicioSesion.Errores = new() { errorMessageGet };
      }
      return (inicioSesion);
    }
  }
}
