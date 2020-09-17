using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Models
{
    public partial class Expenses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int PayerId { get; set; }
        public int RecipientId { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public DateTimeOffset? DeleteTime { get; set; }

        public virtual User Payer { get; set; }
        public virtual User Recipient { get; set; }
    }
}
