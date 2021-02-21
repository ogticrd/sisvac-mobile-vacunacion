using System;
using Newtonsoft.Json;

namespace SisVac.Framework.Domain
{
    public class UserResponse : BaseEntity
    {
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }
        [JsonProperty("cedula")]
        public string Cedula { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("age")]
        public string Age { get; set; }
        [JsonProperty("is_enabled")]
        public string IsEnabled { get; set; }
    }
}
