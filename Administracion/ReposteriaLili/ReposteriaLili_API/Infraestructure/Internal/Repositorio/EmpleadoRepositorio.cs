using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.Internal.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReposteriaLili_API.Infraestructure.Internal.Repositorio
{
    public class EmpleadoRepositorio : Repositorio<Empleado>, IEmpleadoRepositorio
    {
        private readonly ReposteriaDBContext _dBContext;
        private readonly string? _secretKey;

        public EmpleadoRepositorio(ReposteriaDBContext dBContext, string? secretKey) : base(dBContext)
        {
            _dBContext = dBContext;
            _secretKey = secretKey;
        }
    }
}