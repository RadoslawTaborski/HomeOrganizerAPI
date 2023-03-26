using HomeOrganizerAPI.Models;
using System;

namespace HomeOrganizerAPI.Helpers;

public record SaldoExt : Saldo, IModel
{
    public byte[] Uuid { get; set; }
    public DateTimeOffset CreateTime { get; set; }
    public DateTimeOffset? UpdateTime { get; set; }
    public DateTimeOffset? DeleteTime { get; set; }
}
