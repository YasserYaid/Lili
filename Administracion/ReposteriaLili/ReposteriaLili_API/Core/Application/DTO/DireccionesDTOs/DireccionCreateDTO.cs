using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Application.DTO.DireccionesDTOs
{
    public class DireccionCreateDTO
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
        public int? NumeroExterior { get; set; }

        [Required]
        public int? NumeroInterior { get; set; }

        [Required]
        public string? Colonia { get; set; }

        [Required]
        public string? CodigoPostal { get; set; }

        [Required]
        public string? Latitud { get; set; }

        [Required]
        public string? Longitud { get; set; }
    }
}
