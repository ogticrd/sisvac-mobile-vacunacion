using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Refit;

namespace SisVac.Framework.Domain
{
    public class ConsentRequestBody
    {
        [JsonProperty("cedula")]
        public long Cedula { get; set; }

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

        [JsonProperty("hasTransplant")]
        public bool HasTransplant { get; set; }
    }
}
