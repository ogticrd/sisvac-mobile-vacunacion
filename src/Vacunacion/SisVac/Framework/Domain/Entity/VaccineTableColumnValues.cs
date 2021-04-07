using System;
namespace SisVac.Framework.Domain.Dto
{
    public class VaccineStatus
    {
        /**/
        public string Status { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public string Vaccinator { get; set; }
        public string Center { get; set; }
        /*
    <Label Grid.Row="2" Text= "Fecha: --" />
    < Label Grid.Row= "3" Text= "Hora: --" />
    < Label Grid.Row= "4" Text= "Vacunador: --" />
    < Label Grid.Row= "5" Text= "Centro: --" />*/
    }
}
