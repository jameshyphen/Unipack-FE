using System;
using System.Collections.Generic;

namespace Unipack.DTOs
{
    public class PackListDto
    {
        public int PackListId { get; set; }
        public string Name { get; set; }
        public ICollection<ItemDto> Items { get; set; }
        public ICollection<TaskDto> Tasks { get; set; }
        public DateTime AddedOn { get; set; }
    }
}