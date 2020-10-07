using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Repositories
{
    public class TemporaryItemsRepository : Repository<Item, TemporaryItem>
    {
        public TemporaryItemsRepository(HomeOrganizerContext context) : base(context)
        {
        }

        protected override DbSet<Item> Data => _context.Item;

        protected override DbSet<TemporaryItem> DataView => _context.TemporaryItem;
        protected override void CustomGet(ref IQueryable<Item> collection, Parameters parameters)
        {
            collection = collection.Where(i => i.ShoppingListId != null);
            TemporaryItemsResourceParameters castedParams = parameters as TemporaryItemsResourceParameters;
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

        protected override async Task<IEnumerable<Item>> NotQuerableGet(IQueryable<Item> collection)
        {
            IEnumerable<Item> notQuerableCollection = (await collection.ToListAsync()).OrderBy(i => i.CategoryId);
            return notQuerableCollection;
        }
    }
}
