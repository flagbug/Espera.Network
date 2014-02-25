using Newtonsoft.Json.Linq;

namespace Espera.Network
{
    public class NetworkMessage
    {
        public NetworkMessageType MessageType { get; set; }

        public JObject Payload { get; set; }
    }
}