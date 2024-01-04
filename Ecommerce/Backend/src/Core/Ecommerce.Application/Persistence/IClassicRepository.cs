using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Persistence
{
    public interface IClassicRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByNumberIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddALL(IEnumerable<T> entities);
        Task<bool> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(T TEntity);
    }
}
