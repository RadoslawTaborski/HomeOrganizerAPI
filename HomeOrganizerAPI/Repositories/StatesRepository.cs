using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using Dto = HomeOrganizerAPI.Helpers.DTO.State;

namespace HomeOrganizerAPI.Repositories
{
    public class StatesRepository : Repository<State, State, Dto>
    {
        public StatesRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        protected override DbSet<State> Data => _context.State;

        protected override DbSet<State> DataView => _context.State;

        protected override void CustomGet(ref IQueryable<State> collection, Parameters parameters)
        {
        }
    }
}
