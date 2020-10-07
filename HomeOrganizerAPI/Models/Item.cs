using System;

namespace HomeOrganizerAPI.Models
{
    public partial record Item : Model
    {
        public string Name { get; set; }
        public int? ShoppingListId { get; set; }
        public int? StateId { get; set; }
        public string Quantity { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset? Bought { get; set; }

        public virtual Subcategory Category { get; set; }
        public virtual ShoppingList ShoppingList { get; set; }
        public virtual State State { get; set; }
    }
}
