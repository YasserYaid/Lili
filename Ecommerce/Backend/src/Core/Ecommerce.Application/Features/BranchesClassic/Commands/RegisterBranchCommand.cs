using Ecommerce.Application.Features.Auths.Users.Vms;
using MediatR;

namespace Ecommerce.Application.Features.BranchesClassic.Commands
{
    public class RegisterBranchCommand : IRequest<BranchVM>
    {
        public string? Calle { get; set; }
        public string? Ciudad { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Colonia { get; set; }
        public string? DiaInicial { get; set; }
        public string? DiaFinal { get; set; }
        public string? Estado { get; set; }
        public string? HoraInicial { get; set; }
        public string? HoraFinal { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? Municipio { get; set; }
        public string? NombreComercial { get; set; }
        public string? NumeroDepartamento { get; set; }
    }
}
