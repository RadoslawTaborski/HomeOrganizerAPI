using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record Expenses : Model
    {
        public Expenses()
        {
            ExpenseDetails = new HashSet<ExpenseDetails>();
        }

        public byte[] GroupUuid { get; set; }
        public string Name { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<ExpenseDetails> ExpenseDetails { get; set; }
    }
}
