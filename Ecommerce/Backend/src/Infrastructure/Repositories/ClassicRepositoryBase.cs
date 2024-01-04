using Ecommerce.Application.Persistence;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Disposable es para liberar recursos del datacontext es decir para el garbage collector
namespace Ecommerce.Infrastructure.Repositories
{
    public class ClassicRepositoryBase<T> : IClassicRepository<T>, IDisposable where T : class
    {
        protected readonly EcommerceDbContext _context;//En dado caso intentar con la manera clasica
        protected readonly DbSet<T> _dbSet;
        public ClassicRepositoryBase(EcommerceDbContext context) 
        { 
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var items = await _dbSet.ToListAsync();
            return items;
        }

        public async Task<T> GetByNumberIdAsync(int id)
        {
            var item = await _dbSet.FindAsync(id);
            return item;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddALL(IEnumerable<T> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            var item = await _dbSet.FindAsync(id);
            if (item is not null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item is not null)
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> DeleteAsync(T TEntity)
        {
            if (TEntity is not null)
            {
                _context.Remove(TEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
