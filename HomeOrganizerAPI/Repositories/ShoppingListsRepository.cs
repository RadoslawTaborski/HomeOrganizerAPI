using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
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
            var castedParams = parameters as ShoppingListResourceParameters;
            if (!IsNull(castedParams.GroupUuid))
            {
                var arg = castedParams.GroupUuid.Trim();
                collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.GroupUuid);
                if (!IsNull(castedParams.Name))
                {
                    var arg2 = castedParams.Name.Trim();
                    collection = collection.Where(i => i.Name == arg2);
                } else
                {
                    collection = collection.Where(i => !i.Name.Contains("GROUP_"));
                }
            }
            else
            {
                collection = Enumerable.Empty<ShoppingList>().AsAsyncQueryable();
                return;
            }
        }
    }
}
