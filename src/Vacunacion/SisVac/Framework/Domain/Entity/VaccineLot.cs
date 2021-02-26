using System;
namespace SisVac.Framework.Domain
{
    public class VaccineLot : BaseEntity
    {
        public int VaccineBrandLocalId { get; set; }
        public int VaccineBrandId { get; set; }
        public string Name { get; set; }
    }
}
