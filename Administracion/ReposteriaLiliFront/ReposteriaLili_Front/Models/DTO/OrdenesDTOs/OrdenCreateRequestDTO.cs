using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_Front.Models.DTO.OrdenesDTOs
{
    public class OrdenCreateRequestDTO
    {
        [Required]
        public int IdDireccionCliente { get; set; }
        [Required]
        public int IdTarjeta { get; set; }
        [Required]
        public int IdSucursal { get; set; }
    }
}
