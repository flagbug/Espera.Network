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

        public Guid RequestId { get; set; }

        public ResponseStatus Status { get; set; }
    }
}