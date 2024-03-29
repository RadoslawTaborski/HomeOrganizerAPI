﻿using System.Collections.Generic;

namespace HomeOrganizerAPI.Models;

public partial record Category : Model
{
    public Category()
    {
        Subcategory = new HashSet<Subcategory>();
    }

    public byte[] GroupUuid { get; set; }
    public string Name { get; set; }

    public virtual Group Group { get; set; }
    public virtual ICollection<Subcategory> Subcategory { get; set; }
}
