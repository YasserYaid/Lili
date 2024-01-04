using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios.IVM;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;
using ReposteriaLili_API.Infraestructure.Internal.Persistence;

namespace ReposteriaLili_API.Infraestructure.Internal.Repositorio.VM
{
    public class ViewListarProductosRepositorio : Repositorio<ProductoJVM>, IViewListarProductosRepositorio
    {
        private readonly ReposteriaDBContext _dBContext;
        public ViewListarProductosRepositorio(ReposteriaDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }
    }
}
