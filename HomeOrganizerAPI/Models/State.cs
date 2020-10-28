using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record State : Model
    {
        public State()
        {
            Item = new HashSet<Item>();
        }

        public int Level { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Item> Item { get; set; }
    }
}
