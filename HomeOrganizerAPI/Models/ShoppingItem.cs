using System;

namespace HomeOrganizerAPI.Models;

public partial record ShoppingItem : Model
{
    public byte[] GroupUuid { get; set; }
    public string Name { get; set; }
    public byte[] ShoppingListUuid { get; set; }
    public byte[] StateUuid { get; set; }
    public long Counter { get; set; }
    public string Quantity { get; set; }
    public byte[] CategoryUuid { get; set; }
    public DateTimeOffset? Bought { get; set; }
    public bool Visible { get; set; }
    public DateTimeOffset? Archived { get; set; }
}
