using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record Item : Model
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
        public string Quantity { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset? Bought { get; set; }
        public int? ShoppingListId { get; set; }
        public long Counter { get; set; }

        public virtual Subcategory Category { get; set; }
        public virtual Group Group { get; set; }
        public virtual ShoppingList ShoppingList { get; set; }
        public virtual State State { get; set; }
    }
}
