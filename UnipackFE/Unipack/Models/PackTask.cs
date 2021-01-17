using System;
using Unipack.Enums;

namespace Unipack.Models
{
    public class PackTask
    {
        public int PackTaskId { get; set; }

        public string Name { get; set; }
        public User Author { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime Deadline { get; set; }

        public Boolean Completed;

        public Priority Priority;
    }
}