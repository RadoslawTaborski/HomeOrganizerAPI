using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record Subcategory : DtoModel
    {
        [JsonConverter(typeof(IntToStringConventer))]
        public int GroupId { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(IntToStringConventer))]
        public int CategoryId { get; set; }
    }
}
