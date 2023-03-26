using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO;

public record Subcategory : DtoModel
{
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] GroupUuid { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] CategoryUuid { get; set; }
}
