using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record Saldo : Model
    {
        public byte[] Group { get; set; }
        public byte[] Payer { get; set; }
        public byte[] Recipient { get; set; }
        public decimal? Value { get; set; }
    }
}
