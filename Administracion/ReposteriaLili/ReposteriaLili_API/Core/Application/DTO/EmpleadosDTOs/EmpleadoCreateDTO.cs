using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReposteriaLili_API.Core.Application.DTO.EmpleadosDTOs
{
    public class EmpleadoCreateDTO
    {
        [Required]
        public int? IdSucursal { get; set; }

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
        public string? Cargo { get; set; }

        [Required]
        public string? Contrasena { get; set; }
    }
}
