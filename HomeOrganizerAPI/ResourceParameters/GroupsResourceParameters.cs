using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.ResourceParameters
{
    public class GroupsResourceParameters : Parameters
    {
        public override int MaxPageSize { get; set; } = 50;
        public override int PageSize { get; set; } = 25;
        public string UserUuid { get; set; }
    }
}
