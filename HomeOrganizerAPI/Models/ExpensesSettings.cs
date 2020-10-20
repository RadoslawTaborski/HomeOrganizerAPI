using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record ExpensesSettings : Model
    {
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public int GroupId { get; set; }
        public float Value { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
    }
}
