using System;
using Newtonsoft.Json;

namespace SisVac.Framework.Domain
{
    public class VaccineApplication : BaseEntity
    {
        [JsonProperty("cedula")]
        public string Cedula { get; set; }
        [JsonProperty("vaccine")]
        public string Vaccine { get; set; }
        [JsonProperty("lot")]
        public string Lot { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("hour")]
        public string Hour { get; set; }
        [JsonProperty("dose")]
        public string Dose { get; set; }
        [JsonProperty("next_date")]
        public string NextDate{ get; set; }
    }
}
