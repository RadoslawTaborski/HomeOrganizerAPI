using System;
using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO;

public record Item : DtoModel
{
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] GroupUuid { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] ShoppingListUuid { get; set; }
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] StateUuid { get; set; }
    public string Quantity { get; set; }
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] CategoryUuid { get; set; }
    public DateTimeOffset? Bought { get; set; }
    public long Counter { get; set; }
}
