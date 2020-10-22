using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record ShoppingItem : Model
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
        public string Quantity { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset? Bought { get; set; }
        public int? ShoppingListId { get; set; }  
        public long Counter { get; set; }
        public bool Visible { get; set; }
        public DateTimeOffset? Archived { get; set; }
    }
}
