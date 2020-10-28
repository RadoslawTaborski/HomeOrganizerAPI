using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record Expenses : DtoModel
    {
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] GroupUuid { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] PayerUuid { get; set; }
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] RecipientUuid { get; set; }
    }
}
