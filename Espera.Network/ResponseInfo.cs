using Newtonsoft.Json.Linq;
using System;

namespace Espera.Network
{
    public class ResponseInfo
    {
        public JObject Content { get; set; }

        public string Message { get; set; }

        public Guid RequestId { get; set; }

        public ResponseStatus Status { get; set; }
    }
}