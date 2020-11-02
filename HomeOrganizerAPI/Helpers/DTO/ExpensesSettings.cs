using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record ExpensesSettings : DtoModel
    {
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] UserUuid { get; set; }
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] GroupUuid { get; set; }
        public float Value { get; set; }
    }
}
