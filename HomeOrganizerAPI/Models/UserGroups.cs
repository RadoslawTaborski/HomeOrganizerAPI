using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record UserGroups : Model
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
