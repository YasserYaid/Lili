using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.Internal.Persistence;

namespace ReposteriaLili_API.Infraestructure.Internal.Repositorio
{
    public class ProductoOrdenRepositorio : Repositorio<Producto_Orden>, IProductoOrdenRepositorio
    {
        private readonly ReposteriaDBContext _dbContext;
        public ProductoOrdenRepositorio(ReposteriaDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
