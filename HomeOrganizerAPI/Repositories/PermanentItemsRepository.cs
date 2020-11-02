using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dto = HomeOrganizerAPI.Helpers.DTO.PermanentItem;

namespace HomeOrganizerAPI.Repositories
{
    public class PermanentItemsRepository : Repository<Item, PermanentItem, Dto>
    {
        public PermanentItemsRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        protected override DbSet<Item> Data => _context.Item;

        protected override DbSet<PermanentItem> DataView => _context.PermanentItem;
        protected override void CustomGet(ref IQueryable<Item> collection, Parameters parameters)
        {
            collection = collection.Where(i => i.ShoppingListUuid == null);
            var castedParams = parameters as PermanentItemsResourceParameters;
            if (!IsNull(castedParams.GroupUuid))
            {
                var arg = castedParams.GroupUuid.Trim();
                collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.GroupUuid);
            }
            else
            {
                collection = Enumerable.Empty<Item>().AsAsyncQueryable();
                return;
            }
            if (!IsNull(castedParams.SubcategoryUuid))
            {
                var arg = castedParams.SubcategoryUuid.Trim();
                collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.CategoryUuid);
            }
            else if (!IsNull(castedParams.CategoryUuid))
            {
                var arg = castedParams.CategoryUuid.Trim();
                collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.Category.CategoryUuid);
            }

            if (!IsNull(castedParams.StateLevel))
            {
                var arg = castedParams.StateLevel.Trim();
                collection = collection.Where(i => i.State.Level <= int.Parse(arg));
            }
        }

        public override Task<Item> Add(Item element) => throw new NotImplementedException();
        public override Task<bool> DeleteItem(byte[] id) => throw new NotImplementedException();
        public override Task<Item> Update(Item element) => throw new NotImplementedException();
    }
}
