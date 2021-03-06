﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipack.DTOs
{
    public class ItemDto
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public DateTime AddedOn { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Priority { get; set; }
        public Boolean IsCompleted { get; set; }
    }
}
