using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;
using ReposteriaLili_API.Core.Application.DTO.ProductosDTOs;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Application.DTO
{
    public class CartOrderCreateDTO
    {
        [Required]
        public IEnumerable<ProductosOrderRequestDTO> productosOrderRequestDTOs { get; set; }
        [Required]
        public OrdenCreateRequestDTO ordenCreateRequestDTO { get; set; }
    }
}
