using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Transversal.Comun.Respuestas
{
  [JsonObject(MemberSerialization.OptIn)]
  public class RespuestaBase
  {
    [JsonProperty(Order = 1)]
    public int Codigo { get; set; }

    [JsonProperty(Order = 2)]
    public string? Resultado { get; set; }

    [JsonProperty(Order = 3)]
    public string? Mensaje { get; set; }

    [JsonProperty(Order = 4)]
    public IEnumerable<string>? Errores { get; set; }

    public static RespuestaBase Error401(string capa, string mensaje)
    {
      return (new()
      {
        Codigo = 401,
        Resultado = "Error",
        Mensaje = $"No autorizado. Capa de {capa}",
        Errores = new string[] { mensaje }
      });
    }

    public static RespuestaBase Error500(string capa, string mensaje)
    {
      return (new()
      {
        Codigo = 500,
        Resultado = "Error",
        Mensaje = $"Ha ocurrido un error al procesar la solicitud en la capa de {capa}",
        Errores = new string[] { mensaje }
      });
    }

    public static RespuestaBase ConvertirJTokenARespuestaBase(JToken token)
    {
      RespuestaBase respuestaBase = new();

      if (token is JObject objeto)
      {
        respuestaBase.Codigo = objeto.GetValue(nameof(Codigo))?.Value<int>() ?? 0;
        respuestaBase.Resultado = objeto.GetValue(nameof(Resultado))?.Value<string?>();
        respuestaBase.Mensaje = objeto.GetValue(nameof(Mensaje))?.Value<string?>();
      }

      return (respuestaBase);
    }

  }
}
