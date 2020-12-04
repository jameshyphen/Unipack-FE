using System;

namespace Unipack.Models
{
    public class VacationListItem
    {
        public int VacationListItemId { get; set; }
        
        public Item Item { get; set; }
        public VacationList VacationList { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedOn { get; set; }
    }
}