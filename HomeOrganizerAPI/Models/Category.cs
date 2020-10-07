using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record Category : Model
    {
        public Category()
        {
            Subcategory = new HashSet<Subcategory>();
        }

        public string Name { get; set; }

        public virtual ICollection<Subcategory> Subcategory { get; set; }
    }
}
