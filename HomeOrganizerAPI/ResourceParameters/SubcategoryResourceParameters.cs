using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.ResourceParameters
{
    public class SubcategoryResourceParameters : Parameters
    {
        public override int MaxPageSize { get; set; } = 50;
        protected override int PageSize { get; set; } = 50;
        public string CategoryUuid { get; set; }
        public string GroupUuid { get; set; }
    }
}
