using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dto = HomeOrganizerAPI.Helpers.DTO.TemporaryItem;

namespace HomeOrganizerAPI.Repositories
{
    public class TemporaryItemsRepository : Repository<Item, TemporaryItem, Dto>
    {
        public TemporaryItemsRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        protected override DbSet<Item> Data => _context.Item;

        protected override DbSet<TemporaryItem> DataView => _context.TemporaryItem;
        protected override void CustomGet(ref IQueryable<Item> collection, Parameters parameters)
        {
            collection = collection.Where(i => i.ShoppingListId != null);
            var castedParams = parameters as TemporaryItemsResourceParameters;
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
            if (!isNull(castedParams.ShoppingListId))
            {
                var arg = castedParams.ShoppingListId.Trim();
                collection = collection.Where(i => i.ShoppingListId.ToString() == arg);
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
        }
    }
}
