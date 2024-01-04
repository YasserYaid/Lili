using Microsoft.AspNetCore.Mvc.Rendering;
using ReposteriaLili_Front.Models.DTO.EmpleadosDTOs;
using ReposteriaLili_Front.Models.DTO.ProductosDTOs;

namespace ReposteriaLili_Front.Models.ViewModels
{
    public class ProductoViewModel
    {
        public ProductoCreateDTO productoCreateDto { get; set; }
        public IEnumerable<SelectListItem> sucursalesDtoList { get; set; }

        public ProductoViewModel()
        {
            productoCreateDto = new ProductoCreateDTO();
        }
    }
}
