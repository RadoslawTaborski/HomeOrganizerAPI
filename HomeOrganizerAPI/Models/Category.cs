using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            Subcategory = new HashSet<Subcategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public DateTimeOffset? DeleteTime { get; set; }

        public virtual ICollection<Subcategory> Subcategory { get; set; }
    }
}
