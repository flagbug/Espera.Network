using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Espera.Network
{
    public class NetworkMessage
    {
        [JsonProperty(Required = Required.Always)]
        public NetworkMessageType MessageType { get; set; }

        [JsonProperty(Required = Required.Always)]
        public JObject Payload { get; set; }
    }
}