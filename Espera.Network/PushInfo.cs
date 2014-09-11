using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Espera.Network
{
    public class PushInfo
    {
        public JObject Content { get; set; }

        [JsonProperty(Required = Required.Always)]
        public PushAction PushAction { get; set; }
    }
}