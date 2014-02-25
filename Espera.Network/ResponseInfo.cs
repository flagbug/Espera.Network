using Newtonsoft.Json.Linq;

namespace Espera.Network
{
    public class ResponseInfo
    {
        public JObject Content { get; set; }

        public string Message { get; set; }

        public ResponseStatus Status { get; set; }
    }
}