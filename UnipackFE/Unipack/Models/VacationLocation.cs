using System;

namespace Unipack.Models
{
    public class VacationLocation
    {
        public int VacationLocationId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime DateArrival { get; set; }
        public DateTime DateDeparture { get; set; }
    }
}