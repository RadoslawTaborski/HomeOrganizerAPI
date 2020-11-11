using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record UserGroups: DtoModel
    {
        public ExpensesSettings[] ExpensesSettings { get; set; }
    }
}
