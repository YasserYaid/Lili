using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios
{
    public interface IOrdenRepositorio : IRepositorio<Orden>
    {
        Task<Orden?> AsignarRepartidorOrden(int idOrden, int idRepartidor);
        Task<bool> ActualizarOrden(Orden orden);
    }
}
