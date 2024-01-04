using ReposteriaLili_Front.Models.DTO.EmpleadosDTOs;

namespace ReposteriaLili_Front.Models.DTO
{
    public class LoginEmpleadoResponseDTO
    {
        public EmpleadoResponseDTO? EmpleadoResponseDTO { get; set; }
        public string Token { get; set; }
    }
}
