using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record ShoppingList : DtoModel
    {
        [JsonConverter(typeof(IntToStringConventer))]
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
    }
}
