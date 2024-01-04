using ReposteriaLili_Front.Models.DTO.OrdenesDTOs;
using ReposteriaLili_Front.Models.DTO.ProductosDTOs;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_Front.Models.DTO
{
    public class CartOrderCreateDTO
    {
        [Required]
        public IEnumerable<ProductosOrderRequestDTO> productosOrderRequestDTOs { get; set; }
        [Required]
        public OrdenCreateRequestDTO ordenCreateRequestDTO { get; set; }
    }
}
