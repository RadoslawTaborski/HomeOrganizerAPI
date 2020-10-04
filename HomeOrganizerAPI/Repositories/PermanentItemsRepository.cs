using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Repositories
{
    public class PermanentItemsRepository : Repository<Item, PermanentItem>
    {
        public PermanentItemsRepository(HomeOrganizerContext context) : base(context)
        {
        }

        protected override DbSet<Item> Data => _context.Item;

        protected override DbSet<PermanentItem> DataView => _context.PermanentItem;
        protected override void CustomGet(ref IQueryable<Item> collection, Parameters parameters)
        {
            collection = collection.Where(i => i.ShoppingListId == null);
            PermanentItemsResourceParameters castedParams = parameters as PermanentItemsResourceParameters;
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

            if (!isNull(castedParams.StateId))
            {
                var arg = castedParams.StateId.Trim();
                collection = collection.Where(i => i.StateId <= int.Parse(arg));
            }
        }

        protected override async Task<IEnumerable<Item>> NotQuerableGet(IQueryable<Item> collection)
        {
            IEnumerable<Item> notQuerableCollection = (await collection.ToListAsync()).OrderBy(i => i.CategoryId);
            return notQuerableCollection;
        }
    }
}
