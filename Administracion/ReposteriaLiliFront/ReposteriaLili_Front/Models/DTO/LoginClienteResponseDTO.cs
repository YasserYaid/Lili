using ReposteriaLili_Front.Models.DTO.ClientesDTOs;
using ReposteriaLili_Front.Models.DTO.DireccionesDTOs;
using ReposteriaLili_Front.Models.DTO.TarjetasDTOs;

namespace ReposteriaLili_Front.Models.DTO
{
    public class LoginClienteResponseDTO
    {
        public ClienteResponseDTO ClienteResponseDTO { get; set; }
        public TarjetaResponseDTO TarjetaResponseDTO { get; set; }
        public DireccionResponseDTO DireccionResponseDTO { get; set; }
        public string Token { get; set; }
    }
}
