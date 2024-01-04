using ReposteriaLili_API.Core.Application.DTO.ClientesDTOs;
using ReposteriaLili_API.Core.Application.DTO.DireccionesDTOs;
using ReposteriaLili_API.Core.Application.DTO.TarjetasDTOs;

namespace ReposteriaLili_API.Core.Application.DTO
{
    public class LoginClienteResponseDTO
    {
        public ClienteResponseDTO ClienteResponseDTO { get; set; }
        public TarjetaResponseDTO TarjetaResponseDTO { get; set; }
        public DireccionResponseDTO DireccionResponseDTO { get; set; }
        public string Token { get; set; }
    }
}
