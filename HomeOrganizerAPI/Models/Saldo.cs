﻿namespace HomeOrganizerAPI.Models
{
    public partial record Saldo : Model
    {
        public int PayerId { get; set; }
        public int RecipientId { get; set; }
        public decimal Value { get; set; }
    }
}
