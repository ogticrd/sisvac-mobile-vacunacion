using System;
namespace SisVac.Framework.Domain
{
    public class ClinicLocation : BaseEntity
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
