using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO;

public record ExpenseDetails : DtoModel
{
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] ExpenseUuid { get; set; }
    public decimal Value { get; set; }
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] PayerUuid { get; set; }
    [JsonConverter(typeof(GuidToStringConverter))]
    public byte[] RecipientUuid { get; set; }
}
