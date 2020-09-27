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
    public class ShoppingListsRepository : Repository<ShoppingList, ShoppingList>
    {
        public ShoppingListsRepository(HomeOrganizerContext context) : base(context)
        {
        }

        protected override DbSet<ShoppingList> Data => _context.ShoppingList;

        protected override DbSet<ShoppingList> DataView => _context.ShoppingList;

        protected override void CustomGet(ref IQueryable<ShoppingList> collection, Parameters parameters)
        {
        }
    }
}
