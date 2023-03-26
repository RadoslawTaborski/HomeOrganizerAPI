using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

using Dto = HomeOrganizerAPI.Helpers.DTO.ExpenseDetails;

namespace HomeOrganizerAPI.Repositories;

public class ExpenseDetailsRepository : Repository<ExpenseDetails, ExpenseDetails, Dto>
{
    public ExpenseDetailsRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
    {
    }

    protected override DbSet<ExpenseDetails> Data => _context.ExpenseDetails;

    protected override DbSet<ExpenseDetails> DataView => _context.ExpenseDetails;

    protected override void CustomGet(ref IQueryable<ExpenseDetails> collection, Parameters parameters)
    {
        var castedParams = parameters as ExpenseDetialsResourceParameters;
        if (!IsNull(castedParams.GroupUuid))
        {
            var arg = castedParams.GroupUuid.Trim();
            collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.Expense.GroupUuid);
        }
        else
        {
            collection = Enumerable.Empty<ExpenseDetails>().AsAsyncQueryable();
            return;
        }
        if (!IsNull(castedParams.ExpenseUuid))
        {
            var arg = castedParams.ExpenseUuid.Trim();
            collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.ExpenseUuid);
        }
    }
}
