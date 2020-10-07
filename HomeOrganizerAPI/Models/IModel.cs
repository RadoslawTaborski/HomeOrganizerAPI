using System;

namespace HomeOrganizerAPI.Models
{
    public interface IModel
    {
        public int? Id { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public DateTimeOffset? DeleteTime { get; set; }
    }
}
