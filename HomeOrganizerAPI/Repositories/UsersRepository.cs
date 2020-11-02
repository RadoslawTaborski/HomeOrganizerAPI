using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
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
            var castedParams = parameters as UsersResourceParameters;
            if (!IsNull(castedParams.GroupUuid))
            {
                var arg = castedParams.GroupUuid.Trim();
                collection = collection.Where(i => i.UserGroups.Where(o => Guid.Parse(arg).ToByteArray() == o.GroupUuid).Any());
            }
            else
            {
                collection = Enumerable.Empty<User>().AsAsyncQueryable();
                return;
            }
        }

        public async Task<User> Get(string username, string password)
        {
            return await Data.SingleOrDefaultAsync(user => user.Username == username && user.Password == password);
        }
    }
}
