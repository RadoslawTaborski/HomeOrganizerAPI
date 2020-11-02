using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial record Saldo : IEntity
    {
        public byte[] GroupUuid { get; set; }
        public byte[] PayerUuid { get; set; }
        public byte[] RecipientUuid { get; set; }
        public decimal? Value { get; set; }
    }
}
