using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Repositories
{
    public class SubcategoryRepository : Repository<Subcategory, Subcategory>
    {
        public SubcategoryRepository(HomeOrganizerContext context) : base(context)
        {
        }

        protected override DbSet<Subcategory> Data => _context.Subcategory;

        protected override DbSet<Subcategory> DataView => _context.Subcategory;

        protected override void CustomGet(ref IQueryable<Subcategory> collection, Parameters parameters)
        {
        }
    }
}
