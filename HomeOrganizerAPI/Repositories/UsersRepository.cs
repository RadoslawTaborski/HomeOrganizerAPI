using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using Dto = HomeOrganizerAPI.Helpers.DTO.User;

namespace HomeOrganizerAPI.Repositories
{
    public class UsersRepository : Repository<User, User, Dto>
    {
        public UsersRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        protected override DbSet<User> Data => _context.User;

        protected override DbSet<User> DataView => _context.User;

        protected override void CustomGet(ref IQueryable<User> collection, Parameters parameters)
        {
        }
    }
}
