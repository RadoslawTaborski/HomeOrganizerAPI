using System.Collections.Generic;

namespace HomeOrganizerAPI.Models;

public partial record Subcategory : Model
{
    public Subcategory()
    {
        Item = new HashSet<Item>();
    }

    public byte[] GroupUuid { get; set; }
    public string Name { get; set; }
    public byte[] CategoryUuid { get; set; }

    public virtual Category Category { get; set; }
    public virtual Group Group { get; set; }
    public virtual ICollection<Item> Item { get; set; }
}
