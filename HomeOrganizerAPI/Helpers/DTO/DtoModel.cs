using HomeOrganizerAPI.Models;
using System;
using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record DtoModel : IModel
    {
        [JsonConverter(typeof(NullableIntToStringConventer))]
        public int? Id { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public DateTimeOffset? DeleteTime { get; set; }
    }
}
