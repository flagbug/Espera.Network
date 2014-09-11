using System;
using Newtonsoft.Json;

namespace Espera.Network
{
    public class SongTransferInfo
    {
        [JsonProperty(Required = Required.Always)]
        public NetworkSong Metadata { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Guid TransferId { get; set; }
    }
}