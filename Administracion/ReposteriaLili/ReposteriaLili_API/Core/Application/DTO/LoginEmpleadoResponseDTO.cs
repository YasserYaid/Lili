using ReposteriaLili_API.Core.Application.DTO.EmpleadosDTOs;
using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Core.Application.DTO
{
    public class LoginEmpleadoResponseDTO
    {
        public EmpleadoResponseDTO? EmpleadoResponseDTO {  get; set; }
        public string Token { get; set; }
    }
}
