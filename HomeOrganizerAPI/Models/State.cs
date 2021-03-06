﻿using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial class State : Model
    {
        public State()
        {
            Item = new HashSet<Item>();
        }

        public string Name { get; set; }

        public virtual ICollection<Item> Item { get; set; }
    }
}
