using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.ResourceParameters
{
    public abstract class Parameters
    {
        protected abstract int DefaultPageSize {get; set;}

        public abstract int MaxPageSize { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => DefaultPageSize;
            set => DefaultPageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
