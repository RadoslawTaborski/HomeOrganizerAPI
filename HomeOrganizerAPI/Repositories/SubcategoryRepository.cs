using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using Dto = HomeOrganizerAPI.Helpers.DTO.Subcategory;

namespace HomeOrganizerAPI.Repositories
{
    public class SubcategoryRepository : Repository<Subcategory, Subcategory, Dto>
    {
        public SubcategoryRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        protected override DbSet<Subcategory> Data => _context.Subcategory;

        protected override DbSet<Subcategory> DataView => _context.Subcategory;

        protected override void CustomGet(ref IQueryable<Subcategory> collection, Parameters parameters)
        {
            var castedParams = parameters as DefaultParameters;
            if (!isNull(castedParams.GroupId))
            {
                var arg = castedParams.GroupId.Trim();
                collection = collection.Where(i => i.GroupId.ToString() == arg);
            }
            else
            {
                collection = Enumerable.Empty<Subcategory>().AsAsyncQueryable();
                return;
            }
        }
    }
}
