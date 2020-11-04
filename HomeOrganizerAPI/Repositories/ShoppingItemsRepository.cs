using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dto = HomeOrganizerAPI.Helpers.DTO.ShoppingItem;

namespace HomeOrganizerAPI.Repositories
{
    public class ShoppingItemsRepository : Repository<Item, ShoppingItem, Dto>
    {
        public ShoppingItemsRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        protected override DbSet<Item> Data => _context.Item;

        protected override DbSet<ShoppingItem> DataView => _context.ShoppingItem;
        protected override void CustomGet(ref IQueryable<Item> collection, Parameters parameters)
        {
            collection = collection.Where(i => !i.Bought.HasValue);
            var castedParams = parameters as ItemsResourceParameters;
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
            collection = collection.Where(i => i.State.Level <= castedParams.StateLevel || i.StateUuid == null);
        }


        protected override async Task<IEnumerable<Item>> NotQuerableGet(IQueryable<Item> collection)
        {
            IEnumerable<Item> notQuerableCollection = await collection.ToListAsync();
            notQuerableCollection = notQuerableCollection.Where(i => !IsNotVisibleOrArchived(i));
            return notQuerableCollection;
        }

        private bool IsNotVisibleOrArchived(Item i)
        {
            if (i.ShoppingListUuid == null)
            {
                return false;
            }
            else
            {
                var collection = _context.ShoppingList as IQueryable<ShoppingList>;
                var list = collection.Where(l => l.Uuid == i.ShoppingListUuid).FirstOrDefault();
                if (list.DeleteTime.HasValue)
                {
                    return true;
                }
                else
                {
                    return !list.Visible;
                }
            }
        }
    }
}
