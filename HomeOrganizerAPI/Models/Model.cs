using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeOrganizerAPI.Models
{
    public record Model : IModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte[] Uuid { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public DateTimeOffset? DeleteTime { get; set; }
    }
}
