using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

using Dto = HomeOrganizerAPI.Helpers.DTO.Group;

namespace HomeOrganizerAPI.Repositories
{
    public class GroupRepository : Repository<Group, Group, Dto>
    {
        public GroupRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        protected override DbSet<Group> Data => _context.Group;

        protected override DbSet<Group> DataView => _context.Group;

        protected override void CustomGet(ref IQueryable<Group> collection, Parameters parameters)
        {
            var castedParams = parameters as GroupsResourceParameters;
            if (!IsNull(castedParams.UserUuid))
            {
                var arg = castedParams.UserUuid.Trim();
                collection = collection.Where(i => i.UserGroups.Where(o => Guid.Parse(arg).ToByteArray() == o.UserUuid).Any());
            }
            else
            {
                collection = Enumerable.Empty<Group>().AsAsyncQueryable();
                return;
            }
        }

        protected override IQueryable<Group> Extend(DbSet<Group> Data)
        {
            return Data.Include(x => x.UserGroups).ThenInclude(y => y.User);
        }
    }
}
