using ReposteriaLili_Front.Models.DTO.ProductosDTOs;
using ReposteriaLili_Front.Models.DTO.SucursalesDTOs;

namespace ReposteriaLili_Front.Models.DTO
{
    public class CatalogoProductosResponse
    {
        public IEnumerable<ProductoResponseDTO>? ProductoResponseDtoList { get; set; }
        public SucursalResponseDTO? SucursalResponseDto { get; set; }
    }
}
