using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial class User : Model
    {
        public User()
        {
            ExpensesPayer = new HashSet<Expenses>();
            ExpensesRecipient = new HashSet<Expenses>();
            ExpensesSettingsUser1 = new HashSet<ExpensesSettings>();
            ExpensesSettingsUser2 = new HashSet<ExpensesSettings>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Expenses> ExpensesPayer { get; set; }
        public virtual ICollection<Expenses> ExpensesRecipient { get; set; }
        public virtual ICollection<ExpensesSettings> ExpensesSettingsUser1 { get; set; }
        public virtual ICollection<ExpensesSettings> ExpensesSettingsUser2 { get; set; }
    }
}
