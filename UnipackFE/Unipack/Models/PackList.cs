using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class PackList
    {
        public int VacationListId { get; set; }
        public string Name { get; set; }
        public User AuthorUser { get; set; }
        public ICollection<VacationListItem> Items { get; set; }
        public ICollection<VacationTask> Tasks { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
