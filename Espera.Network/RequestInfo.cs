using Newtonsoft.Json.Linq;
using System;

namespace Espera.Network
{
    public class RequestInfo
    {
        public JObject Parameters { get; set; }

        public Guid RequestId { get; set; }
    }
}