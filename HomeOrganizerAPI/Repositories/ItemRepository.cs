using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using Dto = HomeOrganizerAPI.Helpers.DTO.Item;

namespace HomeOrganizerAPI.Repositories
{
    public class ItemRepository : Repository<Item, Item, Dto>
    {
        public ItemRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        protected override DbSet<Item> Data => _context.Item;

        protected override DbSet<Item> DataView => _context.Item;

        protected override void CustomGet(ref IQueryable<Item> collection, Parameters parameters)
        {
            ItemsResourceParameters castedParams = parameters as ItemsResourceParameters;
            if (!isNull(castedParams.GroupId))
            {
                var arg = castedParams.GroupId.Trim();
                collection = collection.Where(i => i.GroupId.ToString() == arg);
            }
            else
            {
                collection = Enumerable.Empty<Item>().AsAsyncQueryable();
                return;
            }
            if (!isNull(castedParams.SubcategoryId))
            {
                var arg = castedParams.SubcategoryId.Trim();
                collection = collection.Where(i => i.CategoryId.ToString() == arg);
            }
            else if (!isNull(castedParams.CategoryId))
            {
                var arg = castedParams.CategoryId.Trim();
                collection = collection.Where(i => i.Category.CategoryId.ToString() == arg);
            }

            collection = collection.Where(i => i.StateId <= castedParams.StateId || i.StateId == null);
        }
    }
}
