using System;
using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO;

public record TemporaryItem : DtoModel
{
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] GroupUuid { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] ShoppingListUuid { get; set; }
    public string Quantity { get; set; }
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] CategoryUuid { get; set; }
    public DateTimeOffset? Bought { get; set; }
}
