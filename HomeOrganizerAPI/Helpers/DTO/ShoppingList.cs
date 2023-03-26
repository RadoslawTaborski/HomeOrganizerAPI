using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO;

public record ShoppingList : DtoModel
{
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] GroupUuid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] CategoryUuid { get; set; }
    public bool Visible { get; set; }
}
