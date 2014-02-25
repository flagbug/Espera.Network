using Newtonsoft.Json.Linq;

namespace Espera.Network
{
    public class PushInfo
    {
        public JObject Content { get; set; }

        public string PushAction { get; set; }
    }
}