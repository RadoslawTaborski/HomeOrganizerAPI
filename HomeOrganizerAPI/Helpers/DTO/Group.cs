using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record Group : DtoModel
    {
        public string Name { get; set; }
    }
}
