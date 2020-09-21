using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Controllers
{
    public class ResponseData
    {
        public object[] data { get; set; }
        public int total { get; set; }
        public string message { get; set; }
        public string error { get; set; }
    }
}
