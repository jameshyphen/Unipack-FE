using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipack.DTOs
{
    public class VacationDto
    {
        public int VacationId { get; set; }
        public string Name { get; set; }
        public ICollection<VacationLocationDto>  Locations { get; set; }
        public ICollection<PackListDto> PackLists { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime DateDeparture { get; set; }
        public DateTime DateReturn { get; set; }
    }
}
