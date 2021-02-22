using System;
using Newtonsoft.Json;

namespace SisVac.Framework.Domain
{
    public class UserResponse : BaseEntity
    {
        [JsonProperty("valid")]
        public bool IsValid { get; set; }
        [JsonProperty("cedula")]
        public string Cedula { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
    }
}
