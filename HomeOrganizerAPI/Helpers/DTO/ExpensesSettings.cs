using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO;

public record ExpensesSettings : DtoModel
{
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] UserUuid { get; set; }
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] GroupUuid { get; set; }
    public float Value { get; set; }
}
