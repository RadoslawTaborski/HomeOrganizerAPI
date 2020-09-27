using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Repositories
{
    public class StatesRepository : Repository<State, State>
    {
        public StatesRepository(HomeOrganizerContext context) : base(context)
        {
        }

        protected override DbSet<State> Data => _context.State;

        protected override DbSet<State> DataView => _context.State;

        protected override void CustomGet(ref IQueryable<State> collection, Parameters parameters)
        {
        }
    }
}
