using ReposteriaLili_Front.Models.DTO.ClientesDTOs;
using ReposteriaLili_Front.Models.DTO.DireccionesDTOs;
using ReposteriaLili_Front.Models.DTO.TarjetasDTOs;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_Front.Models.DTO
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
