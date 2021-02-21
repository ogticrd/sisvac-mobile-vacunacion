using System;
using Newtonsoft.Json;

namespace SisVac.Framework.Domain
{
    public class BaseEntity
    {
        public int LocalId { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
