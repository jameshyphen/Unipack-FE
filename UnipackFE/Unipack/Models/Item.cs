using System;

namespace Unipack.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public User Author { get; set; }
        public DateTime AddedOn { get; set; }
        public Priority Priority { get; set; }
        public Category Category { get; set; }
    }
}