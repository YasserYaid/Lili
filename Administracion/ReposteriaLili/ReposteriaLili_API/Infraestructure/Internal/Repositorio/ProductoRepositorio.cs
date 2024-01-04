using Microsoft.EntityFrameworkCore;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.Internal.Persistence;

namespace ReposteriaLili_API.Infraestructure.Internal.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ReposteriaDBContext _dBContext;
        public ProductoRepositorio(ReposteriaDBContext dBContext) : base(dBContext) 
        {
            _dBContext = dBContext;
        }

        public async Task<bool> ActualizarCantidad(int idProducto, int cantidad)
        {
            bool esCantidadActualizada = false;

            Producto? producto = _dBContext.Productos.FirstOrDefault(proord => proord.IdProducto == idProducto);

            if (producto != null) producto.CantidadDisponible = cantidad;

            int filasAfectadas = await _dBContext.SaveChangesAsync();

            if (filasAfectadas > 0) esCantidadActualizada = true;

            return esCantidadActualizada;
        }
    }
}
