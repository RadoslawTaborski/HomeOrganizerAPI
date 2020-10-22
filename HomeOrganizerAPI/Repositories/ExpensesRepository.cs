using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using Dto = HomeOrganizerAPI.Helpers.DTO.Expenses;

namespace HomeOrganizerAPI.Repositories
{
    public class ExpensesRepository : Repository<Expenses, Expenses, Dto>
    {
        public ExpensesRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        protected override DbSet<Expenses> Data => _context.Expenses;

        protected override DbSet<Expenses> DataView => _context.Expenses;

        protected override void CustomGet(ref IQueryable<Expenses> collection, Parameters parameters)
        {
            var castedParams = parameters as DefaultParameters;
            if (!isNull(castedParams.GroupId))
            {
                var arg = castedParams.GroupId.Trim();
                collection = collection.Where(i => i.GroupId.ToString() == arg);
            }
            else
            {
                collection = Enumerable.Empty<Expenses>().AsAsyncQueryable();
                return;
            }
        }
    }
}
