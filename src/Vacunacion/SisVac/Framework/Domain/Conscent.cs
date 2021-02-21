using System;
using Newtonsoft.Json;

namespace SisVac.Framework.Domain
{
    public class Conscent : BaseEntity
    {
        [JsonProperty("cedula")]
        public string Cedula { get; set; }
        [JsonProperty("has_covid")]
        public bool HasCovid { get; set; }
        [JsonProperty("is_pregnant")]
        public bool IsPregnant { get; set; }
        [JsonProperty("had_fever")]
        public bool HadFever { get; set; }
        [JsonProperty("is_vaccinated")]
        public bool IsVaccinated { get; set; }
        [JsonProperty("had_reactions")]
        public bool HadReactions { get; set; }
        [JsonProperty("is_allergic")]
        public bool IsAllergic { get; set; }
        [JsonProperty("is_medicated")]
        public bool IsMedicated { get; set; }
        [JsonProperty("had_transplats")]
        public bool HadTransplant { get; set; }
    }
}
