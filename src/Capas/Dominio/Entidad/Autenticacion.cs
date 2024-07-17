using Newtonsoft.Json;

namespace Dominio.Entidad
{
  public class Autenticacion
  {
    internal static Autenticacion context;

    public class EmpresaInfo
    {
      [JsonProperty("enterpriseToken", Required = Required.Always)]
      public string? enterpriseToken { get; set; }

      [JsonProperty("entepriseId", Required = Required.Always)]
      public string? entepriseId { get; set; }

      [JsonProperty("enterpriseNit", Required = Required.Always)]
      public string? enterpriseNit { get; set; }

      [JsonProperty("enterpriseschemeid", Required = Required.Always)]
      public string? enterpriseschemeid { get; set; }

      [JsonProperty("enviroment", Required = Required.Always)]
      public string? enviroment { get; set; }
    }
    [JsonProperty("user", Required = Required.Always)]
    public EmpresaInfo? user { get; set; }

    public string ToJson()
    {
      return JsonConvert.SerializeObject(this);

    }
    public static Autenticacion FromJson(string data)
    {
      return JsonConvert.DeserializeObject<Autenticacion>(data)!;
    }
  }
}
