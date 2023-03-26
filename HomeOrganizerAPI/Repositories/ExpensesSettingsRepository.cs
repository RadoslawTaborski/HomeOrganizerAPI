using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.ExpensesSettings;

namespace HomeOrganizerAPI.Repositories;

public class ExpensesSettingsRepository : Repository<ExpensesSettings, ExpensesSettings, Dto>
{
    public ExpensesSettingsRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
    {
    }

    protected override DbSet<ExpensesSettings> Data => _context.ExpensesSettings;

    protected override DbSet<ExpensesSettings> DataView => _context.ExpensesSettings;

    protected override void CustomGet(ref IQueryable<ExpensesSettings> collection, Parameters parameters)
    {
        var castedParams = parameters as ExpensesSettingsResourceParameters;
        if (!IsNull(castedParams.GroupUuid))
        {
            var arg = castedParams.GroupUuid.Trim();
            collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.UserGroups.GroupUuid);
        }
        else
        {
            collection = Enumerable.Empty<ExpensesSettings>().AsAsyncQueryable();
            return;
        }
    }

    protected override async Task<IEnumerable<ExpensesSettings>> NotQuerableGet(IQueryable<ExpensesSettings> collection)
    {
        return await collection.Include(i => i.UserGroups).ToListAsync();
    }
}