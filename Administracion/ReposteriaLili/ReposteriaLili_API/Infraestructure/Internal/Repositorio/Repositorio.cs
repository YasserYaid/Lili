using Microsoft.EntityFrameworkCore;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.Internal.Persistence;
using System.Linq.Expressions;

namespace ReposteriaLili_API.Infraestructure.Internal.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ReposteriaDBContext _dbContext;
        internal DbSet<T> dbSet;

        public Repositorio(ReposteriaDBContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<T>();
        }

        public async Task Eliminar(T entidad)
        {
            dbSet.Remove(entidad);
            await SalvarCambios();
        }

        public async Task<T?> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filtro != null)
            {
                query = query.Where(filtro);   
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.ToListAsync();
        }

        public async Task Registrar(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await SalvarCambios();
        }

        public async Task<T> RegistrarRetornandoConID(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await SalvarCambios();
            return entidad;
        }

        public async Task<T> UpdateAttachRetornando(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateRetornando(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAttach(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task SalvarCambios()
        {
            await _dbContext.SaveChangesAsync();
        }

        ////////////////////////////////////////////////

        public async Task<T?> ObtenerById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task EliminarRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);
            await SalvarCambios();
        }

        public async Task<T?> ObtenerPrimero(Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null, bool isTracking = true)
        {

            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);   //  select /* from where ....
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);   
                }
            }

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ObtenerTodosOrder(Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);   //  select /* from where ....
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);    // 
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }


        /*
       public PagedList<T> ObtenerTodosPaginado(Parametros parametros, Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
       {
           IQueryable<T> query = dbSet;
           if (filtro != null)
           {
               query = query.Where(filtro);   //  select /* from where ....
           }
           if (incluirPropiedades != null)
           {
               foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
               {
                   query = query.Include(incluirProp);    //  ejemplo "Categoria,Marca"
               }
           }
           if (orderBy != null)
           {
               query = orderBy(query);
           }
           if (!isTracking)
           {
               query = query.AsNoTracking();
           }
           return PagedList<T>.ToPagedList(query, parametros.PageNumber, parametros.PageSize);
       }
*/

    }
}
