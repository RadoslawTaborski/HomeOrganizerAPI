using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record UserGroups : Model
    {
        public byte[] UserUuid { get; set; }
        public byte[] GroupUuid { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
