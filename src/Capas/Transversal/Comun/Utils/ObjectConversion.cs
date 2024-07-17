using Newtonsoft.Json;

namespace Transversal.Comun.Utils
{
  public class ObjectConversion<T>
  {
    public static String ToJson(T o)
    {
      return JsonConvert.SerializeObject(o).Replace(":null", ":\"\"");
    }

    public static T FromJson(String json)
    {
      return JsonConvert.DeserializeObject<T>(json)!;
    }

    public static String ObjToJson(T o)
    {
      return JsonConvert.SerializeObject(o);
    }

    internal static void FromJson(object p)
    {
      throw new NotImplementedException();
    }
  }
}
