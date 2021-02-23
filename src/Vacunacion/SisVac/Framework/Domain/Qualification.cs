using Newtonsoft.Json;

namespace SisVac.Framework.Domain
{
    public class Qualification
    {
        [JsonProperty("valid")]
        public bool Valid { get; set; }

        [JsonProperty("isEnabled")]
        public bool IsEnabled { get; set; }

        [JsonProperty("cedula")]
        public string Cedula { get; set; }
    }
}
