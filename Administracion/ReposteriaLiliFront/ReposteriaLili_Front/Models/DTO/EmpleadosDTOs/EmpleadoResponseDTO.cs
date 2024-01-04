using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReposteriaLili_Front.Models.DTO.EmpleadosDTOs
{
    public class EmpleadoResponseDTO
    {
        public int IdEmpleado { get; set; }
        public int? IdSucursal { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? UserName { get; set; }
        public string? Cargo { get; set; }
        public string? Contrasena { get; set; }
        public DateTime? FechaIngreso { get; set; }
    }
}
