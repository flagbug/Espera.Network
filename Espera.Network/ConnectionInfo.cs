using System;
using Newtonsoft.Json;

namespace Espera.Network
{
    public class ConnectionInfo
    {
        [JsonProperty(Required = Required.Always)]
        public NetworkAccessPermission AccessPermission { get; set; }

        [JsonProperty(Required = Required.Always)]
        public GuestSystemInfo GuestSystemInfo { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Version ServerVersion { get; set; }
    }
}