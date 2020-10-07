using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeOrganizerAPI.Repositories
{
    public class UsersRepository : Repository<User, User>
    {
        public UsersRepository(HomeOrganizerContext context) : base(context)
        {
        }

        protected override DbSet<User> Data => _context.User;

        protected override DbSet<User> DataView => _context.User;

        protected override void CustomGet(ref IQueryable<User> collection, Parameters parameters)
        {
        }
    }
}
