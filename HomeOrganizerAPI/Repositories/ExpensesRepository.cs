﻿using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeOrganizerAPI.Repositories
{
    public class ExpensesRepository : Repository<Expenses, Expenses>
    {
        public ExpensesRepository(HomeOrganizerContext context) : base(context)
        {
        }

        protected override DbSet<Expenses> Data => _context.Expenses;

        protected override DbSet<Expenses> DataView => _context.Expenses;

        protected override void CustomGet(ref IQueryable<Expenses> collection, Parameters parameters)
        {
        }
    }
}
