using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record Expenses : DtoModel
    {
        [JsonConverter(typeof(IntToStringConventer))]
        public int GroupId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        [JsonConverter(typeof(IntToStringConventer))]
        public int PayerId { get; set; }
        [JsonConverter(typeof(IntToStringConventer))]
        public int RecipientId { get; set; }
    }
}
