using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record ListCategory : DtoModel
    {
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] GroupUuid { get; set; }
        public string Name { get; set; }
    }
}
