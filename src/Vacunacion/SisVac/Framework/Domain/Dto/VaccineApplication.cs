using System;
using Newtonsoft.Json;

namespace SisVac.Framework.Domain
{
    public class VaccineApplication : BaseEntity
    {
        [JsonProperty("cedula")]
        public string Cedula{ get; set; }
        [JsonProperty("citizen")]
        public Citizen Citizen { get; set; }
        [JsonProperty("vaccine")]
        public string Vaccine { get; set; }
        [JsonProperty("lot")]
        public string Lot { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("location_id")]
        public string LocationId { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("hour")]
        public string Hour { get; set; }
        [JsonProperty("dose")]
        public string Dose { get; set; }
        [JsonProperty("vaccinator_manager_cedula")]
        public string VaccinatorManagerCedula { get; set; }
        [JsonProperty("vaccinator_cedula")]
        public string VaccinatorCedula { get; set; }
        [JsonProperty("vaccine_id")]
        public string VaccineId { get; set; }
        [JsonProperty("lot_id")]
        public string LotId { get; set; }
    }
}
