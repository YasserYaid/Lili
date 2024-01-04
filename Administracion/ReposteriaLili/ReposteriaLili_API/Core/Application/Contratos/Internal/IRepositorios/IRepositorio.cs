using System.Linq.Expressions;

namespace ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios
{
    public interface IRepositorio<T> where T : class
    {
        Task Registrar(T entidad);
        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null);
        Task<T?> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true);
        Task Eliminar(T entidad);
        Task SalvarCambios();
        Task<T> RegistrarRetornandoConID(T entidad);
        Task<T> UpdateAttachRetornando(T entity);//PREFERIR ATTACH
        Task<T> UpdateRetornando(T entity);
        Task UpdateAttach(T entity);//PREFERIR ATTACH
        Task Update(T entity);

        /////////////////////////////////////////////////////////////////////////////////////

        Task<T?> ObtenerById(int id);
        Task<IEnumerable<T>> ObtenerTodosOrder(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );
        Task<T?> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );
        /*
        PagedList<T> ObtenerTodosPaginado(Parametros parametros, Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null,
            bool isTracking = true);
        */
        Task EliminarRango(IEnumerable<T> entidad);
    }
}
