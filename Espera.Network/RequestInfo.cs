using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Espera.Network
{
    public class RequestInfo
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public JObject Parameters { get; set; }

        public RequestAction RequestAction { get; set; }

        public Guid RequestId { get; set; }
    }
}