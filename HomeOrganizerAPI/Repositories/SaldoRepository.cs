﻿using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeOrganizerAPI.Repositories
{
    public class SaldoRepository : Repository<Saldo, Saldo>
    {
        public SaldoRepository(HomeOrganizerContext context) : base(context)
        {
        }

        protected override DbSet<Saldo> Data => _context.Saldo;

        protected override DbSet<Saldo> DataView => _context.Saldo;

        protected override void CustomGet(ref IQueryable<Saldo> collection, Parameters parameters)
        {
            var castedParams = parameters as DefaultParameters;
            if (!isNull(castedParams.GroupId))
            {
                var arg = castedParams.GroupId.Trim();
                collection = collection.Where(i => i.GroupId.ToString() == arg);
            }
            else
            {
                collection = Enumerable.Empty<Saldo>().AsAsyncQueryable();
                return;
            }
        }
    }
}
