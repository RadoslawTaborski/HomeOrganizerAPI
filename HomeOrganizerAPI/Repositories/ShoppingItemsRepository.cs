using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Repositories
{
    public class ShoppingItemsRepository : Repository<Item, ShoppingItem>
    {
        public ShoppingItemsRepository(HomeOrganizerContext context) : base(context)
        {
        }

        protected override DbSet<Item> Data => _context.Item;

        protected override DbSet<ShoppingItem> DataView => _context.ShoppingItem;
        protected override void CustomGet(ref IQueryable<Item> collection, Parameters parameters)
        {
            collection = collection.Where(i => !i.Bought.HasValue);
            ItemsResourceParameters castedParams = parameters as ItemsResourceParameters;
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


        protected override async Task<IEnumerable<Item>> NotQuerableGet(IQueryable<Item> collection)
        {
            IEnumerable<Item> notQuerableCollection = await collection.ToListAsync();
            notQuerableCollection = notQuerableCollection.Where(i => !IsNotVisibleOrArchived(i));
            return notQuerableCollection;
        }

        private bool IsNotVisibleOrArchived(Item i)
        {
            if (i.ShoppingListId == null)
            {
                return false;
            } else
            {
                var collection = _context.ShoppingList as IQueryable<ShoppingList>;
                var list = collection.Where(l => l.Id == i.ShoppingListId).FirstOrDefault();
                if (list.DeleteTime.HasValue)
                {
                    return true;
                } else
                {
                    return !list.Visible;
                }
            }
        }
    }
}
