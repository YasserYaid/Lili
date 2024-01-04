using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Application.DTO.TarjetasDTOs
{
    public class TarjetaCreateDTO
    {
        [Required]
        public int? CodigoCVV { get; set; }

        [Required]
        public string? Emisor { get; set; }

        [Required]
        public DateTime? FechaVencimiento { get; set; }

        [Required]
        public string? NombreTitular { get; set; }

        [Required]
        public string? NumeroTarjeta { get; set; }

        [Required]
        public string? TipoTarjeta { get; set; }
    }
}
