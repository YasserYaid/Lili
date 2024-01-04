using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReposteriaLili_Front.Models.DTO.EmpleadosDTOs
{
    public class EmpleadoCreateDTO
    {
        [Required(ErrorMessage ="Campo requerido")]
        public int? IdSucursal { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string? ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string? ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string? Cargo { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string? Contrasena { get; set; }
    }
}
