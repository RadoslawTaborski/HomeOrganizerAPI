using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO;

public record Saldo : IDtoEntity
{
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] GroupUuid { get; set; }
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] PayerUuid { get; set; }
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] RecipientUuid { get; set; }
    public decimal Value { get; set; }
}
