using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.ResourceParameters
{
    public class ExpensesSettingsResourceParameters : Parameters
    {
        public override int MaxPageSize { get; set; } = 50;
        public override int PageSize { get; set; } = 50;
        public string UserUuid { get; set; }
        public string GroupUuid { get; set; }
    }
}
