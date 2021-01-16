using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipack.DTOs
{
    public class VacationLocationDto
    {
        public int VacationLocationId { get; set; }

        public string CityName { get; set; }
        public string CountryName { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime DateArrival { get; set; }
        public DateTime DateDeparture { get; set; }
    }
}
