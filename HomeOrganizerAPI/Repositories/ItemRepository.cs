using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
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
            if (!isNull(castedParams.GroupUuid))
            {
                var arg = castedParams.GroupUuid.Trim();
                collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.GroupUuid);
            }
            else
            {
                collection = Enumerable.Empty<Item>().AsAsyncQueryable();
                return;
            }
            if (!isNull(castedParams.SubcategoryUuid))
            {
                var arg = castedParams.SubcategoryUuid.Trim();
                collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.CategoryUuid);
            }
            else if (!isNull(castedParams.CategoryUuid))
            {
                var arg = castedParams.CategoryUuid.Trim();
                collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.Category.CategoryUuid);
            }

            collection = collection.Where(i => i.State.Level <= castedParams.StateLevel || i.StateUuid == null);
        }
    }
}
