using System;
using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record TemporaryItem : DtoModel
    {
        [JsonConverter(typeof(IntToStringConventer))]
        public int GroupId { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(NullableIntToStringConventer))]
        public int? ShoppingListId { get; set; }
        public string Quantity { get; set; }
        [JsonConverter(typeof(IntToStringConventer))]
        public int CategoryId { get; set; }
        public DateTimeOffset? Bought { get; set; }
    }
}
