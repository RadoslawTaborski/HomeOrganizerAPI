using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record ExpenseDetails : DtoModel
    {
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] ExpenseUuid { get; set; }
        public decimal Value { get; set; }
        public User Payer { get; set; }
        public User Recipient { get; set; }
    }
}
