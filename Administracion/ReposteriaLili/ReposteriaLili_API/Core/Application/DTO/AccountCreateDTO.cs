using ReposteriaLili_API.Core.Application.DTO.ClientesDTOs;
using ReposteriaLili_API.Core.Application.DTO.DireccionesDTOs;
using ReposteriaLili_API.Core.Application.DTO.TarjetasDTOs;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Application.DTO
{
    public class AccountCreateDTO
    {
        [Required]
        public ClienteCreateDTO? ClienteCreateDto { get; set; }
        [Required]        
        public DireccionCreateDTO? DireccionCreateDto { get; set; }        
        [Required]
        public TarjetaCreateDTO? TarjetaCreateDTO { get; set; }
    }
}
