using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial class PermanentItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public DateTimeOffset? DeleteTime { get; set; }
    }
}
