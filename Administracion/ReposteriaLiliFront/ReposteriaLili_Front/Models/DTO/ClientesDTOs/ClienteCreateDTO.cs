using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_Front.Models.DTO.ClientesDTOs
{
    public class ClienteCreateDTO
    {
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? ApellidoPaterno { get; set; }
        [Required]
        public string? ApellidoMaterno { get; set; }
        [Required]
        public string? Correo { get; set; }
        [Required]
        public string? Telefono { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Contrasena { get; set; }
        [Required]
        public DateTime? FechaNacimiento { get; set; }
    }
}
