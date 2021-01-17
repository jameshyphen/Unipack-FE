using System;
using System.Collections.Generic;

namespace Unipack.DTOs
{
    public class PackListDto
    {
        public int PackListId { get; set; }
        public string Name { get; set; }
        public ICollection<PackItemDto> Items { get; set; }
        public ICollection<PackTaskDto> Tasks { get; set; }
        public DateTime AddedOn { get; set; }
    }
}