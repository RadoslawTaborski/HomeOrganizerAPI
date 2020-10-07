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
        }
    }
}
