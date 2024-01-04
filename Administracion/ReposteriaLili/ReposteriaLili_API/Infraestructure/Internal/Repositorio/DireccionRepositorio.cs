using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.Internal.Persistence;

namespace ReposteriaLili_API.Infraestructure.Internal.Repositorio
{
    public class DireccionRepositorio : Repositorio<Direccion>, IDireccionRepositorio
    {
        private readonly ReposteriaDBContext _dBContext;
        public DireccionRepositorio(ReposteriaDBContext dBContext) : base(dBContext) 
        {
            _dBContext = dBContext;
        }
    }
}
