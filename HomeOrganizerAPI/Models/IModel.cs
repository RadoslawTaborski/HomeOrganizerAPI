using System;

namespace HomeOrganizerAPI.Models
{
    public interface IModel : IEntity
    {
        public byte[] Uuid { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public DateTimeOffset? DeleteTime { get; set; }
    }
}
