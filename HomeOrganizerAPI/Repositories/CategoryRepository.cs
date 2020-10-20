using HomeOrganizerAPI.Helpers;
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
            var castedParams = parameters as DefaultParameters;
            if (!isNull(castedParams.GroupId))
            {
                var arg = castedParams.GroupId.Trim();
                collection = collection.Where(i => i.GroupId.ToString() == arg);
            } 
            else
            {
                collection = Enumerable.Empty<Category>().AsAsyncQueryable();
                return;
            }
        }
    }
}
