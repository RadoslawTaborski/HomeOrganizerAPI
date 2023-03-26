using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.Saldo;

namespace HomeOrganizerAPI.Repositories;

public class SaldoRepository
{
    private readonly HomeOrganizerContext _context;
    private readonly IPropertyMappingService _propertyMappingService;
    private DbSet<Saldo> Data => _context.Saldo;

    public SaldoRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService)
    {
        this._context = context ?? throw new ArgumentNullException(nameof(context));
        this._propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
    }

    private void CustomGet(ref IQueryable<Saldo> collection, Parameters parameters)
    {
        var castedParams = parameters as SaldoResourceParameters;
        if (!IsNull(castedParams.GroupUuid))
        {
            var arg = castedParams.GroupUuid.Trim();
            collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.GroupUuid);
        }
        else
        {
            collection = Enumerable.Empty<Saldo>().AsAsyncQueryable();
            return;
        }
    }

    public async Task<(IEnumerable<Saldo> Collection, int Lenght)> Get(Parameters parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        var collection = Data as IQueryable<Saldo>;

        if (!IsNull(parameters.OrderBy))
        {
            var propertyMappingDirectory = _propertyMappingService.GetPropertyMapping<Dto, Saldo>();
            collection = collection.ApplySort(parameters.OrderBy, propertyMappingDirectory);
        }

        CustomGet(ref collection, parameters);

        var lenght = await collection.CountAsync();

        return (collection.Skip(parameters.DefaultPageSize * (parameters.PageNumber - 1)).Take(parameters.DefaultPageSize), lenght);
    }
    private bool IsNull(string data)
    {
        return string.IsNullOrWhiteSpace(data);
    }
}
