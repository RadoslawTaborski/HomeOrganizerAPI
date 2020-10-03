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

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }

        public virtual ICollection<Item> Item { get; set; }
    }
}
