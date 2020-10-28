using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record Item : Model
    {
        public byte[] GroupUuid { get; set; }
        public string Name { get; set; }
        public byte[] StateUuid { get; set; }
        public string Quantity { get; set; }
        public byte[] CategoryUuid { get; set; }
        public DateTimeOffset? Bought { get; set; }
        public byte[] ShoppingListUuid { get; set; }
        public long Counter { get; set; }

        public virtual Subcategory Category { get; set; }
        public virtual Group Group { get; set; }
        public virtual ShoppingList ShoppingList { get; set; }
        public virtual State State { get; set; }
    }
}
