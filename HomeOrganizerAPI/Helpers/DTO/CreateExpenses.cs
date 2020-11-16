using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public class CreateExpenses : IDtoEntity
    {
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] GroupUuid { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(GuidToStringConverter))]
        public byte[] Payer { get; set; }
        [JsonConverter(typeof(GuidToStringConverter))]
        public List<byte[]> Recipients { get; set; }
        public decimal Amount { get; set; }
        public bool FiftyFifty { get; set; }
    }
}
