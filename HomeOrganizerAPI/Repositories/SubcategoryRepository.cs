using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

using Dto = HomeOrganizerAPI.Helpers.DTO.Subcategory;

namespace HomeOrganizerAPI.Repositories;

public class SubcategoryRepository : Repository<Subcategory, Subcategory, Dto>
{
    public SubcategoryRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
    {
    }

    protected override DbSet<Subcategory> Data => _context.Subcategory;

    protected override DbSet<Subcategory> DataView => _context.Subcategory;

    protected override void CustomGet(ref IQueryable<Subcategory> collection, Parameters parameters)
    {
        var castedParams = parameters as SubcategoryResourceParameters;
        if (!IsNull(castedParams.GroupUuid))
        {
            var arg = castedParams.GroupUuid.Trim();
            collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.GroupUuid);
        }
        else
        {
            collection = Enumerable.Empty<Subcategory>().AsAsyncQueryable();
            return;
        }
        if (!IsNull(castedParams.CategoryUuid))
        {
            var arg = castedParams.CategoryUuid.Trim();
            collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.CategoryUuid);
        }
    }
}
