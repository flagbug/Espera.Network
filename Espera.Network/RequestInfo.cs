using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Espera.Network
{
    public class RequestInfo
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public JObject Parameters { get; set; }

        public string RequestAction { get; set; }

        public Guid RequestId { get; set; }
    }
}