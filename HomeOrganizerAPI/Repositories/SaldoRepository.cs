using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

using Dto = HomeOrganizerAPI.Helpers.DTO.Saldo;

namespace HomeOrganizerAPI.Repositories
{
    public class SaldoRepository : Repository<Saldo, Saldo, Dto>
    {
        public SaldoRepository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        protected override DbSet<Saldo> Data => _context.Saldo;

        protected override DbSet<Saldo> DataView => _context.Saldo;

        protected override void CustomGet(ref IQueryable<Saldo> collection, Parameters parameters)
        {
            var castedParams = parameters as DefaultParameters;
            if (!isNull(castedParams.GroupUuid))
            {
                var arg = castedParams.GroupUuid.Trim();
                collection = collection.Where(i => Guid.Parse(arg).ToByteArray() == i.Group);
            }
            else
            {
                collection = Enumerable.Empty<Saldo>().AsAsyncQueryable();
                return;
            }
        }
    }
}
