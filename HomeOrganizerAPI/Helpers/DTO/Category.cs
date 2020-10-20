using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record Category : DtoModel
    {
        [JsonConverter(typeof(IntToStringConventer))]
        public int GroupId { get; set; }
        public string Name { get; set; }
    }
}
