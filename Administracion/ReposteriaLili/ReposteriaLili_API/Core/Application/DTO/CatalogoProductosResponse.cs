using ReposteriaLili_API.Core.Application.DTO.ClientesDTOs;
using ReposteriaLili_API.Core.Application.DTO.DireccionesDTOs;
using ReposteriaLili_API.Core.Application.DTO.ProductosDTOs;
using ReposteriaLili_API.Core.Application.DTO.SucursalesDTOs;

namespace ReposteriaLili_API.Core.Application.DTO
{
    public class CatalogoProductosResponse
    {
        public IEnumerable<ProductoResponseDTO>? ProductoResponseDtoList { get; set; }
        public SucursalResponseDTO? SucursalResponseDto { get; set; }
    }
}
