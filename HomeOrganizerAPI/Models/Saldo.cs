using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial class Saldo
    {
        public int PayerId { get; set; }
        public int RecipientId { get; set; }
        public decimal Value { get; set; }
    }
}
