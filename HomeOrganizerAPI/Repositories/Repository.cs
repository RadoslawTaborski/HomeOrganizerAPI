using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Repositories
{
    public abstract class Repository<T, V> 
        where T: Model 
        where V: Model
    {
        protected readonly HomeOrganizerContext _context;

        protected abstract DbSet<T> Data { get; }

        protected abstract DbSet<V> DataView { get; }

        protected abstract void CustomGet(ref IQueryable<T> collection, Parameters parameters);

        public Repository(HomeOrganizerContext context)
        {
            _context = context;
        }

        public async Task<T> Get(int id)
        {
            return await Data.FindAsync(id);
        }

        public async Task<IEnumerable<V>> Get()
        {
            return await DataView.ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(Parameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var collection = Data as IQueryable<T>;

            CustomGet(ref collection, parameters);

            return await collection.Skip(parameters.PageSize * (parameters.PageNumber - 1)).Take(parameters.PageSize).ToListAsync();
        }

        public async Task<bool> Add(T element)
        {
            Data.Add(element);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(T element)
        {
            element.UpdateTime = DateTimeOffset.Now;
            _context.Entry(element).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var entity = await Data.FindAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException("entity not found");
            }

            entity.UpdateTime = DateTimeOffset.Now;
            entity.DeleteTime = DateTimeOffset.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await Data.FindAsync(id);
            return entity != null;
        }

        protected bool isNull(string data)
        {
            return string.IsNullOrWhiteSpace(data);
        }
    }
}
