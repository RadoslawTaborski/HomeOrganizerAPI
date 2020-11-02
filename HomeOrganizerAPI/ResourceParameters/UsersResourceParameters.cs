using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.ResourceParameters
{
    public class UsersResourceParameters : Parameters
    {
        public override int MaxPageSize { get; set; } = 50;
        protected override int PageSize { get; set; } = 25;
        public string GroupUuid { get; set; }
    }
}
