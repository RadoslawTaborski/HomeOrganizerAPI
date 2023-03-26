using System;

namespace HomeOrganizerAPI.Helpers.DTO;

interface IDtoModel : IDtoEntity
{
    public byte[] Uuid { get; set; }
    public DateTimeOffset CreateTime { get; set; }
    public DateTimeOffset? UpdateTime { get; set; }
    public DateTimeOffset? DeleteTime { get; set; }
}
