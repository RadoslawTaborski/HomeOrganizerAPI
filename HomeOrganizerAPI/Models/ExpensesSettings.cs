using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial class ExpensesSettings
    {
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public float Value { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public DateTimeOffset? DeleteTime { get; set; }

        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
    }
}
