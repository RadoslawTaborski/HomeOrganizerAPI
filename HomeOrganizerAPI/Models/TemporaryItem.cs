using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record TemporaryItem : Model
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int? ShoppingListId { get; set; }
        public string Quantity { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset? Bought { get; set; }
    }
}
