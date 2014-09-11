using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Espera.Network
{
    public class ResponseInfo
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public JObject Content { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Guid RequestId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public ResponseStatus Status { get; set; }
    }
}