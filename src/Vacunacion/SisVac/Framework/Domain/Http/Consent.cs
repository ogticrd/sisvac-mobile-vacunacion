using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Refit;

namespace SisVac.Framework.Domain
{
    public class Consent
    {
        [JsonProperty("citizen")]
        public Citizen Citizen { get; set; }

        [JsonProperty("cedula")]
        public string Cedula { get; set; }

        [JsonProperty("hasCovid")]
        public bool HasCovid { get; set; }

        [JsonProperty("isPregnant")]
        public bool IsPregnant { get; set; }

        [JsonProperty("hadFever")]
        public bool HadFever { get; set; }

        [JsonProperty("isVaccinated")]
        public bool IsVaccinated { get; set; }

        [JsonProperty("hadReactions")]
        public bool HadReactions { get; set; }

        [JsonProperty("isAllergic")]
        public bool IsAllergic { get; set; }

        [JsonProperty("isMedicated")]
        public bool IsMedicated { get; set; }

        [JsonProperty("isInmunoDeficient")]
        public bool IsInmunoDeficient { get; set; }

        [JsonProperty("hasTransplant")]
        public bool HasTransplant { get; set; }
    }

    public class Citizen
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("cedula")]
        public string Document { get; set; }
    }
}
