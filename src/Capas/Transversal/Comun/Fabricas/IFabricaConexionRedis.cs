using TFHKA.RedisCacheLibrary;

namespace Transversal.Comun.Fabricas
{
  public interface IFabricaConexionRedis
  {
    public IRedisCache Cache { get; }
  }
}
