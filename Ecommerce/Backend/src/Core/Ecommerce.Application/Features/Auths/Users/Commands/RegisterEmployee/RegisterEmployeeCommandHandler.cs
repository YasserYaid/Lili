
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Auths.Users.Commands.RegisterUser;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Identity;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.RegisterEmployee
{
    public class RegisterEmployeeCommandHandler : IRequestHandler<RegisterEmployeeCommand, AuthResponse>
    {
        private readonly UserManager<Usuario> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IAuthService _authService;

        public RegisterEmployeeCommandHandler(UserManager<Usuario> userManager,RoleManager<IdentityRole> roleManager,IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existeUserByEmail = await _userManager.FindByEmailAsync(request.email!) is null ? false : true;
            if (existeUserByEmail)
            {
                throw new BadRequestException("El Email del usuario ya existe en la base de datos");
            }

            var existeUserByUsername = await _userManager.FindByNameAsync(request.username!) is null ? false : true;
            if (existeUserByUsername)
            {
                throw new BadRequestException("El Username del usuario ya existe en la base de datos");
            }

            if (request.cargo == null)
            {
                throw new Exception("No se puede registrar el usuario sin elegir un cargo");
            }

            var usuario = new Usuario
            {
                Nombre = request.nombre,
                Apellido = request.apellido,
                Telefono = request.telefono,
                Email = request.email,
                UserName = request.username,
                AvatarUrl = request.fotoUrl,
            };

            var resultado = await _userManager.CreateAsync(usuario!, request.password!);

            if (resultado.Succeeded)
            {
                Console.WriteLine("****************Entro*******************");
                string rol = AppRole.GenericUser;
                if (request.cargo.Equals("REPARTIDOR"))
                {
                    rol = AppRole.Deliveryman;
                }else if (request.cargo.Equals("GERENTE-VENTAS"))
                {
                    rol = AppRole.SalesManager;
                }
                else if (request.cargo.Equals("ADMINISTRADOR"))
                {
                    rol = AppRole.Admin;
                }
                await _userManager.AddToRoleAsync(usuario, rol);
                var roles = await _userManager.GetRolesAsync(usuario);

                return new AuthResponse
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Telefono = usuario.Telefono,
                    Email = usuario.Email,
                    Username = usuario.UserName,
                    Avatar = usuario.AvatarUrl,
                    Token = _authService.CreateToken(usuario, roles),
                    Roles = roles
                };
            }

            throw new Exception("No se pudo registrar el empleado");

        }
    }
}
