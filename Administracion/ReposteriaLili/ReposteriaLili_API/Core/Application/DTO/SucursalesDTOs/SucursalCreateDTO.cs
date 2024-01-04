using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReposteriaLili_API.Core.Application.DTO.SucursalesDTOs
{
    public class SucursalCreateDTO
    {
        [Required]
        public string? Estado { get; set; }

        [Required]
        public string? Ciudad { get; set; }

        [Required]
        public string? Municipio { get; set; }

        [Required]
        public string? Calle { get; set; }

        [Required]
        public int? Numero { get; set; }

        [Required]
        public string? Colonia { get; set; }

        [Required]
        public string? CodigoPostal { get; set; }

        [Required]
        public string? Latitud { get; set; }

        [Required]
        public string? Longitud { get; set; }

        [Required]
        public string? DiaInicial { get; set; }

        [Required]
        public string? DiaFinal { get; set; }

        [Required]
        public TimeSpan? HoraInicial { get; set; }

        [Required]
        public TimeSpan? HoraFinal { get; set; }

        [Required]
        public string? NombreComercial { get; set; }
    }
}
