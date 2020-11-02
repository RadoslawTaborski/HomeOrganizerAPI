using HomeOrganizerAPI.Models;
using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record ExpensesSettings : Model
    {
        public byte[] UserGroupsUuid { get; set; }
        public float Value { get; set; }

        public virtual UserGroups UserGroups { get; set; }
    }
}
