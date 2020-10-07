using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeOrganizerAPI.Repositories
{
    public class CategoryRepository : Repository<Category, Category>
    {
        public CategoryRepository(HomeOrganizerContext context) : base(context)
        {
        }

        protected override DbSet<Category> Data => _context.Category;

        protected override DbSet<Category> DataView => _context.Category;

        protected override void CustomGet(ref IQueryable<Category> collection, Parameters parameters)
        {
        }
    }
}
