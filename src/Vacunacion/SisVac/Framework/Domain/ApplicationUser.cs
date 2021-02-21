using System;
namespace SisVac.Framework.Domain
{
    public class ApplicationUser : Person
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
    }
}