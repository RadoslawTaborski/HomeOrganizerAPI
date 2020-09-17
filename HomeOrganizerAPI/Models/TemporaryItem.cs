using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial class TemporaryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ShoppingListId { get; set; }
        public string Quantity { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset? Bought { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public DateTimeOffset? DeleteTime { get; set; }
    }
}
