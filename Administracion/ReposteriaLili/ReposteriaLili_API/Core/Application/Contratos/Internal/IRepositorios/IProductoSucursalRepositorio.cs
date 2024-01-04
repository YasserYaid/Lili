using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios
{
    public interface IProductoSucursalRepositorio : IRepositorio<Producto_Sucursal>
    {
        Task<bool> ActualizarCantidad(int idProducto, int idSucursal, int cantidad);

    }
}
