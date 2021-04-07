using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SisVac.Framework.Domain.Http
{
    public class EffectsReport
    {
        [JsonProperty("cedula")]
        public string Cedula { get; set; }

        [JsonProperty("had_fever")]
        public bool HadFever { get; set; }

        [JsonProperty("had_chills")]
        public bool HadChills { get; set; }

        [JsonProperty("had_tiredness")]
        public bool HadTiredness { get; set; }

        [JsonProperty("had_headache")]
        public bool HadHeadache { get; set; }

        [JsonProperty("had_nausea")]
        public bool HadNausea { get; set; }

        [JsonProperty("had_dizziness")]
        public bool HadDizziness { get; set; }

        [JsonProperty("had_other")]
        public bool HadOther { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }
    }
}
