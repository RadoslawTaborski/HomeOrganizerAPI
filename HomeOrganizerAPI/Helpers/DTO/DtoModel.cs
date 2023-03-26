using System;
using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO;

public record DtoModel : IDtoModel
{
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] Uuid { get; set; }
    public DateTimeOffset CreateTime { get; set; }
    public DateTimeOffset? UpdateTime { get; set; }
    public DateTimeOffset? DeleteTime { get; set; }
}
