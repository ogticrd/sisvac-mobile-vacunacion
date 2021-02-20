using System;
namespace SisVac.Framework.Domain
{
    public class ApplicationUser
    {
        public string Document { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
    }
}