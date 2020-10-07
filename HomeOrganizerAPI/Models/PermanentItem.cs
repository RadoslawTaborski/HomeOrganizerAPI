﻿namespace HomeOrganizerAPI.Models
{
    public partial record PermanentItem : Model
    {
        public string Name { get; set; }
        public int? StateId { get; set; }
        public int CategoryId { get; set; }
    }
}
