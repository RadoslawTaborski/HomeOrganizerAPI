using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record ExpensesSettings : Model
    {
        public byte[] User1Uuid { get; set; }
        public byte[] User2Uuid { get; set; }
        public byte[] GroupUuid { get; set; }
        public float Value { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
    }
}
