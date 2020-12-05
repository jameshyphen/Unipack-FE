using System;
using System.Collections.Generic;

namespace Unipack.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public User Author { get; set; }
        public ICollection<Item> Items { get; set; }
        public DateTime AddedOn { get; set; }
    }
}