using System;

namespace HomeOrganizerAPI.Models;

public partial record TemporaryItem : Model
{
    public byte[] GroupUuid { get; set; }
    public string Name { get; set; }
    public byte[] ShoppingListUuid { get; set; }
    public string Quantity { get; set; }
    public byte[] CategoryUuid { get; set; }
    public DateTimeOffset? Bought { get; set; }
}
