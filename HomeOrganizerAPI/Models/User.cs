using System.Collections.Generic;

namespace HomeOrganizerAPI.Models;

public partial record User : Model
{
    public User()
    {
        ExpenseDetailsPayer = new HashSet<ExpenseDetails>();
        ExpenseDetailsRecipient = new HashSet<ExpenseDetails>();
        UserGroups = new HashSet<UserGroups>();
    }

    public string Username { get; set; }
    public string ExternalUuid { get; set; }
    public virtual ICollection<ExpenseDetails> ExpenseDetailsPayer { get; set; }
    public virtual ICollection<ExpenseDetails> ExpenseDetailsRecipient { get; set; }
    public virtual ICollection<UserGroups> UserGroups { get; set; }
}
