using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

using Dto = HomeOrganizerAPI.Helpers.DTO.Category;

namespace HomeOrganizerAPI.Repositories;

public class CategoryRepository : Repository<Category, Category, Dto>
{
    public CategoryRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
    {
    }

    protected override DbSet<Category> Data => _context.Category;

    protected override DbSet<Category> DataView => _context.Category;

    protected override void CustomGet(ref IQueryable<Category> collection, Parameters parameters)
    {
        var castedParams = parameters as CategoryResourceParameters;
        if (!IsNull(castedParams.GroupUuid))
        {
            var arg = castedParams.GroupUuid.Trim();
            collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.GroupUuid);
        } 
        else
        {
            collection = Enumerable.Empty<Category>().AsAsyncQueryable();
            return;
        }
    }
}
