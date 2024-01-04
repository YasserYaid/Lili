using Microsoft.EntityFrameworkCore;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.Internal.Persistence;

namespace ReposteriaLili_API.Infraestructure.Internal.Repositorio
{
    public class ProductoSucursalRepositorio : Repositorio<Producto_Sucursal>, IProductoSucursalRepositorio
    {
        private readonly ReposteriaDBContext _dbContext;
        public ProductoSucursalRepositorio(ReposteriaDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ActualizarCantidad(int idProducto, int idSucursal, int cantidad)
        {
            bool esCantidadActualizada = false;
            
            Producto_Sucursal? producto_sucursal_table = _dbContext.Productos_Sucursales.FirstOrDefault(proord => proord.IdProducto == idProducto && proord.IdSucursal == idSucursal);
            
            if (producto_sucursal_table != null) producto_sucursal_table.Cantidad = cantidad;
            
            int filasAfectadas = await _dbContext.SaveChangesAsync();
            
            if (filasAfectadas > 0) esCantidadActualizada = true;
            
            return esCantidadActualizada;
        }
    }
}
