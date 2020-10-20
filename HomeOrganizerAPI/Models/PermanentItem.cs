using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record PermanentItem : Model
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
        public int CategoryId { get; set; }
    }
}
