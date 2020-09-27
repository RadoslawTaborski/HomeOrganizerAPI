using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial class Subcategory : Model
    {
        public Subcategory()
        {
            Item = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Item> Item { get; set; }
    }
}
