﻿namespace HomeOrganizerAPI.Models
{
    public partial record Expenses : Model
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int PayerId { get; set; }
        public int RecipientId { get; set; }

        public virtual User Payer { get; set; }
        public virtual User Recipient { get; set; }
    }
}
