using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
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
        public byte? Visible { get; set; }
        public DateTimeOffset? Archived { get; set; }
    }
}
