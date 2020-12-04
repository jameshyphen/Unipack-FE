using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class Vacation
    {
        public int VacationId { get; set; }
        public string Name { get; set; }
        public ICollection<VacationLocation> Locations { get; set; }
        public ICollection<VacationList> VacationLists { get; set; }
        public DateTime DateDeparture { get; set; }
        public DateTime DateReturn { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
