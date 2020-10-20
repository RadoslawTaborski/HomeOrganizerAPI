using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record Saldo : DtoModel
    {
        [JsonConverter(typeof(IntToStringConventer))]
        public int GroupId { get; set; }
        [JsonConverter(typeof(IntToStringConventer))]
        public int PayerId { get; set; }
        [JsonConverter(typeof(IntToStringConventer))]
        public int RecipientId { get; set; }
        public decimal Value { get; set; }
    }
}
