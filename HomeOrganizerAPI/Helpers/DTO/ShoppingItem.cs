using System;
using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record ShoppingItem : DtoModel
    {
        [JsonConverter(typeof(IntToStringConventer))]
        public int GroupId { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(NullableIntToStringConventer))]
        public int? StateId { get; set; }
        [JsonConverter(typeof(IntToStringConventer))]
        public int CategoryId { get; set; }
        [JsonConverter(typeof(NullableIntToStringConventer))]
        public int? ShoppingListId { get; set; }
        public string Quantity { get; set; }
        public DateTimeOffset? Bought { get; set; }
        public bool? Visible { get; set; }
        public long Counter { get; set; }
        public DateTimeOffset? Archived { get; set; }
    }
}
