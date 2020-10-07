using HomeOrganizerAPI.Models;
using System;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record DtoModel : IModel
    {
        public int? Id { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public DateTimeOffset? DeleteTime { get; set; }
    }
}
