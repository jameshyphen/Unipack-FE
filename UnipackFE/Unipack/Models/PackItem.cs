using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class PackItem
    {
        public int PackItemId { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int PackListId { get; set; }
        public PackList PackList { get; set; }
        public int PackedQuantity { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
