using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial class ShoppingList : Model
    {
        public ShoppingList()
        {
            Item = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Item> Item { get; set; }
    }
}
