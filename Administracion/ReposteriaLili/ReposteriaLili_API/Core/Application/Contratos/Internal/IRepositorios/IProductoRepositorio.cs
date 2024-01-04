using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios
{
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        Task<bool> ActualizarCantidad(int idProducto, int cantidad);
    }
}
