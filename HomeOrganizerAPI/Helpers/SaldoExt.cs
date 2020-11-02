using HomeOrganizerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Helpers
{
    public record SaldoExt : Saldo, IModel
    {
        public byte[] Uuid { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public DateTimeOffset? DeleteTime { get; set; }
    }
}
