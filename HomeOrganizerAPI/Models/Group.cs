using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record Group : Model
    {
        public Group()
        {
            Category = new HashSet<Category>();
            ListCategory = new HashSet<ListCategory>();
            Expenses = new HashSet<Expenses>();
            Item = new HashSet<Item>();
            ShoppingList = new HashSet<ShoppingList>();
            Subcategory = new HashSet<Subcategory>();
            UserGroups = new HashSet<UserGroups>();
        }

        public string Name { get; set; }

        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<ListCategory> ListCategory { get; set; }
        public virtual ICollection<Expenses> Expenses { get; set; }
        public virtual ICollection<Item> Item { get; set; }
        public virtual ICollection<ShoppingList> ShoppingList { get; set; }
        public virtual ICollection<Subcategory> Subcategory { get; set; }
        public virtual ICollection<UserGroups> UserGroups { get; set; }
    }
}
