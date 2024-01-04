using ReposteriaLili_Front.Models.DTO.ClientesDTOs;
using ReposteriaLili_Front.Models.DTO.DireccionesDTOs;
using ReposteriaLili_Front.Models.DTO.TarjetasDTOs;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_Front.Models.DTO
{
    public class AccountResponseDTO
    {
        public ClienteResponseDTO? ClienteResponseDto { get; set; }
        public DireccionResponseDTO? DireccionResponseDto { get; set; }
        public TarjetaResponseDTO? TarjetaResponseDTO { get; set; }
    }
}
