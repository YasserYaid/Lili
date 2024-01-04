using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_Front.Models.ViewModels
{
    public class SucursalViewModel
    {
        public int IdSucursal { get; set; }

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
        public string? HoraInicial { get; set; }

        [Required]
        public string? HoraFinal { get; set; }

        [Required]
        public string? NombreComercial { get; set; }
    }
}
