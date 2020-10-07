﻿using System;

namespace HomeOrganizerAPI.Models
{
    public partial record TemporaryItem : Model
    {
        public string Name { get; set; }
        public int? ShoppingListId { get; set; }
        public string Quantity { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset? Bought { get; set; }
    }
}
