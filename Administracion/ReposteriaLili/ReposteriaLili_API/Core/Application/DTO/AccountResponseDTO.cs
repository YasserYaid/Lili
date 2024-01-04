using ReposteriaLili_API.Core.Application.DTO.ClientesDTOs;
using ReposteriaLili_API.Core.Application.DTO.DireccionesDTOs;
using ReposteriaLili_API.Core.Application.DTO.TarjetasDTOs;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Application.DTO
{
    public class AccountResponseDTO
    {
        public ClienteResponseDTO? ClienteResponseDto { get; set; }
        public DireccionResponseDTO? DireccionResponseDto { get; set; }
        public TarjetaResponseDTO? TarjetaResponseDTO { get; set; }
    }
}
