using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Application.DTO
{
    public class LoginClienteRequestDTO
    {
        [Required]
        public string? Telefono { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
