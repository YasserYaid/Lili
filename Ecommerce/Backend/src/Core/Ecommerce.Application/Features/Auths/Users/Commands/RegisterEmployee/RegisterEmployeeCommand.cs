using Ecommerce.Application.Features.Auths.Users.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Auths.Users.Commands.RegisterEmployee
{
    public class RegisterEmployeeCommand : IRequest<AuthResponse>
    {
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? telefono { get; set; }
        public string? email { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public DateTime? date { get; set; }//FechaIngreso
        public string? cargo { get; set; }
        public IFormFile? foto { get; set; }
        public string? fotoUrl { get; set; }
        public string? fotoId { get; set; }
    }
}
