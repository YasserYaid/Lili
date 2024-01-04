using Microsoft.EntityFrameworkCore;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios.IVM;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;
using ReposteriaLili_API.Infraestructure.Internal.Persistence;

namespace ReposteriaLili_API.Infraestructure.Internal.Repositorio.VM
{
    public class ViewSesionClienteRepositorio : Repositorio<ClienteJVM>, IViewSesionClienteRepositorio
    {
        private readonly ReposteriaDBContext _dBContext;
        public ViewSesionClienteRepositorio(ReposteriaDBContext dbContext) : base(dbContext)
        {
            _dBContext = dbContext;
        }
    }
}
