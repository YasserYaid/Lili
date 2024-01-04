using Microsoft.AspNetCore.Mvc.Rendering;
using ReposteriaLili_Front.Models.DTO.EmpleadosDTOs;
using ReposteriaLili_Front.Models.DTO.OrdenesDTOs;

namespace ReposteriaLili_Front.Models.ViewModels
{
    public class OrdenViewModel
    {



        public OrdenUpdateDTO OrdenUpdateDTO { get; set; }
        public IEnumerable<SelectListItem> repartidoresDtoList { get; set; }

        public OrdenViewModel()
        {
            OrdenUpdateDTO = new OrdenUpdateDTO();
        }

    }
}
