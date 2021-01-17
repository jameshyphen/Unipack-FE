using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class PackList
    {
        public int PackListId { get; set; }
        public string Name { get; set; }
        public User AuthorUser { get; set; }
        public ICollection<PackItem> PackItems { get; set; }
        public ICollection<PackTask> Tasks { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
