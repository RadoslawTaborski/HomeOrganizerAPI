using System;
using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record ShoppingItem : DtoModel
    {
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] GroupUuid { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] StateUuid { get; set; }
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] CategoryUuid { get; set; }
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] ShoppingListUuid { get; set; }
        public string Quantity { get; set; }
        public DateTimeOffset? Bought { get; set; }
        public bool? Visible { get; set; }
        public long Counter { get; set; }
        public DateTimeOffset? Archived { get; set; }
    }
}
