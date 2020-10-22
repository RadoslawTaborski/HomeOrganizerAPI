using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using Dto = HomeOrganizerAPI.Helpers.DTO.ShoppingList;

namespace HomeOrganizerAPI.Repositories
{
    public class ShoppingListsRepository : Repository<ShoppingList, ShoppingList, Dto>
    {
        public ShoppingListsRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        protected override DbSet<ShoppingList> Data => _context.ShoppingList;

        protected override DbSet<ShoppingList> DataView => _context.ShoppingList;

        protected override void CustomGet(ref IQueryable<ShoppingList> collection, Parameters parameters)
        {
            var castedParams = parameters as DefaultParameters;
            if (!isNull(castedParams.GroupId))
            {
                var arg = castedParams.GroupId.Trim();
                collection = collection.Where(i => i.GroupId.ToString() == arg);
            }
            else
            {
                collection = Enumerable.Empty<ShoppingList>().AsAsyncQueryable();
                return;
            }
        }
    }
}
