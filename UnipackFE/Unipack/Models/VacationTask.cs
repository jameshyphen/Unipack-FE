using System;
using Unipack.Enums;

namespace Unipack.Models
{
    public class VacationTask
    {
        public int VacationTaskId { get; set; }

        public string Name { get; set; }
        public User Author { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime DeadLine { get; set; }

        public Boolean Completed;

        public Priority Priority;
    }
}