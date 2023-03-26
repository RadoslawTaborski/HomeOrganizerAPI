using System.Collections.Generic;

namespace HomeOrganizerAPI.Models;

public partial record ListCategory : Model
{
    public ListCategory()
    {
        ShoppingList = new HashSet<ShoppingList>();
    }

    public byte[] GroupUuid { get; set; }
    public string Name { get; set; }

    public virtual Group Group { get; set; }
    public virtual ICollection<ShoppingList> ShoppingList { get; set; }
}
