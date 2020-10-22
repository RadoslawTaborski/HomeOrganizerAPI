using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Helpers.DTO;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Repositories
{
    public abstract class Repository<T, V, DTO> : IDisposable
        where T : Model
        where V : Model
        where DTO : DtoModel
    {
        protected readonly HomeOrganizerContext _context;
        protected readonly IPropertyMappingService _propertyMappingService;

        protected abstract DbSet<T> Data { get; }

        protected abstract DbSet<V> DataView { get; }

        protected abstract void CustomGet(ref IQueryable<T> collection, Parameters parameters);

        protected async virtual Task<IEnumerable<T>> NotQuerableGet(IQueryable<T> collection)
        {
            return await collection.ToListAsync();
        }

        public Repository(HomeOrganizerContext context, IPropertyMappingService propertyMappingService)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public async Task<T> Get(int id)
        {
            return await Data.FindAsync(id);
        }

        public async Task<(IEnumerable<V>, int)> Get()
        {
            IEnumerable<V> collection = await DataView.ToListAsync();

            return (collection, collection.Count());
        }

        public async Task<(IEnumerable<T> Collection, int Lenght)> Get(Parameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var collection = Data as IQueryable<T>;

            collection = collection.Where(i => !i.DeleteTime.HasValue);

            if (!isNull(parameters.OrderBy))
            {
                var propertyMappingDirectory = _propertyMappingService.GetPropertyMapping<DTO, T>();
                collection = collection.ApplySort(parameters.OrderBy, propertyMappingDirectory);
            }

            CustomGet(ref collection, parameters);

            var enumerable = await NotQuerableGet(collection);
            var lenght = enumerable.Count();

            return (enumerable.Skip(parameters.DefaultPageSize * (parameters.PageNumber - 1)).Take(parameters.DefaultPageSize), lenght);
        }

        public async Task<T> Add(T element)
        {
            element.CreateTime = DateTimeOffset.Now;
            Data.Add(element);
            await _context.SaveChangesAsync();

            return element;
        }

        public async Task<T> Update(T element)
        {
            element.UpdateTime = DateTimeOffset.Now;
            _context.Entry(element).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return element;
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

        public async Task<bool> Exists(int? id)
        {
            var entity = await Data.FindAsync(id);
            return entity != null;
        }

        protected bool isNull(string data)
        {
            return string.IsNullOrWhiteSpace(data);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
