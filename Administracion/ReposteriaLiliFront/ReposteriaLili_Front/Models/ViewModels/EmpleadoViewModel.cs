using Microsoft.AspNetCore.Mvc.Rendering;
using ReposteriaLili_Front.Models.DTO.EmpleadosDTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReposteriaLili_Front.Models.ViewModels
{
    public class EmpleadoViewModel
    {
        public EmpleadoCreateDTO EmpleadocreateDto { get; set; }
        public IEnumerable<SelectListItem> sucursalesDtoList { get; set; }

        public EmpleadoViewModel()
        {
            EmpleadocreateDto = new EmpleadoCreateDTO();
        }

    }
}
