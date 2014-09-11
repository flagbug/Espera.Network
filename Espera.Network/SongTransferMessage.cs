using System;
using Newtonsoft.Json;

namespace Espera.Network
{
    public class SongTransferMessage
    {
        [JsonProperty(Required = Required.Always)]
        public byte[] Data { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Guid TransferId { get; set; }
    }
}