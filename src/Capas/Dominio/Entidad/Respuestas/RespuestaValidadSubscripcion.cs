using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Comun.Respuestas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dominio.Entidad.Respuestas
{

    [JsonObject(MemberSerialization.OptIn)]
    public class RespuestaValidadSubscripcion : RespuestaBase
    {
    [JsonProperty(Order = 4)]
    public string? IdSuscripcion { get; set; }
    }
}
