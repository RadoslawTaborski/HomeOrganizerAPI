using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

using Dto = HomeOrganizerAPI.Helpers.DTO.ListCategory;

namespace HomeOrganizerAPI.Repositories
{
    public class ListCategoryRepository : Repository<ListCategory, ListCategory, Dto>
    {
        public ListCategoryRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        protected override DbSet<ListCategory> Data => _context.ListCategory;

        protected override DbSet<ListCategory> DataView => _context.ListCategory;

        protected override void CustomGet(ref IQueryable<ListCategory> collection, Parameters parameters)
        {
            var castedParams = parameters as ListCategoryResourceParameters;
            if (!IsNull(castedParams.GroupUuid))
            {
                var arg = castedParams.GroupUuid.Trim();
                collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.GroupUuid);
            } 
            else
            {
                collection = Enumerable.Empty<ListCategory>().AsAsyncQueryable();
                return;
            }
        }
    }
}
