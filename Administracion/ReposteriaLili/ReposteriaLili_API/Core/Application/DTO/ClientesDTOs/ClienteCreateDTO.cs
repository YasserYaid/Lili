using ReposteriaLili_API.Core.Dominio;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ReposteriaLili_API.Core.Application.DTO.TarjetasDTOs;
using ReposteriaLili_API.Core.Application.DTO.DireccionesDTOs;

namespace ReposteriaLili_API.Core.Application.DTO.ClientesDTOs
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
