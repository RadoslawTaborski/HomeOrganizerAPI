using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (!IsNull(castedParams.GroupUuid))
            {
                var arg = castedParams.GroupUuid.Trim();
                collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.GroupUuid);
            }
            else
            {
                collection = Enumerable.Empty<Expenses>().AsAsyncQueryable();
                return;
            }
            collection = collection.Where(i => i.ExpenseDetails.Sum(p => p.Value) != 0);
        }

        protected override async Task<IEnumerable<Expenses>> NotQuerableGet(IQueryable<Expenses> collection)
        {
            return await collection
                .Include(c => c.ExpenseDetails).ThenInclude(c => c.Payer)
                .Include(c => c.ExpenseDetails).ThenInclude(c => c.Recipient).ThenInclude(c => c.UserGroups).ThenInclude(c => c.ExpensesSettings)
                .ToListAsync();
        }

        protected override void BeforeCreate(Expenses element)
        {
            SetValues(element);

            foreach (var detail in element.ExpenseDetails)
            {
                SetValues(detail);
            }
        }

        private static void SetValues(Model element)
        {
            element.CreateTime = DateTime.Now;
            element.Uuid = Guid.NewGuid().ToByteArray();
        }
    }
}
