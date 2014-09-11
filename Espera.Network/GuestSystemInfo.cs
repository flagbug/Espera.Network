using Newtonsoft.Json;

namespace Espera.Network
{
    public class GuestSystemInfo
    {
        [JsonProperty(Required = Required.Always)]
        public bool IsEnabled { get; set; }

        public int RemainingVotes { get; set; }
    }
}