using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Repositories
{
    public class ItemRepository : Repository<Item, Item>
    {
        public ItemRepository(HomeOrganizerContext context) : base(context)
        {
        }

        protected override DbSet<Item> Data => _context.Item;

        protected override DbSet<Item> DataView => _context.Item;

        protected override void CustomGet(ref IQueryable<Item> collection, Parameters parameters)
        {
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
    }
}
