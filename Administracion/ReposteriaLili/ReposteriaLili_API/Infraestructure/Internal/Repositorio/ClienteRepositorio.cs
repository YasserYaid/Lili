using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.Internal.Persistence;

namespace ReposteriaLili_API.Infraestructure.Internal.Repositorio
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        private readonly ReposteriaDBContext _dBContext;
        public ClienteRepositorio(ReposteriaDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }
    }
}
