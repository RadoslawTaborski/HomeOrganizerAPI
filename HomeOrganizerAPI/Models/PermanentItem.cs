using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial class PermanentItem : Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
        public int CategoryId { get; set; }
    }
}
