using Microsoft.Extensions.Configuration;
using TFHKA.RedisCacheLibrary;
using Transversal.Comun.Fabricas;

namespace Infraestructura.Datos.Fabricas
{
  public class FabricaConexionRedisCache : IFabricaConexionRedis
  {
    private readonly IConfiguration _configuracion;

    public FabricaConexionRedisCache(IConfiguration configuracion)
    {
      _configuracion = configuracion;
    }

    public IRedisCache Cache { get { return (new RedisCache(_configuracion)); } }
  }
}
