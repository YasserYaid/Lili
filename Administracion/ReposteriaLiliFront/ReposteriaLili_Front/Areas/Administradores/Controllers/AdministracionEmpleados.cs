using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ReposteriaLili_Front.Models.DTO.EmpleadosDTOs;
using ReposteriaLili_Front.Models.DTO.SucursalesDTOs;
using ReposteriaLili_Front.Models.ReqResModels;
using ReposteriaLili_Front.Models.ViewModels;
using ReposteriaLili_Front.Services.IServices;

namespace ReposteriaLili_Front.Areas.Administradores.Controllers
{
    [Area("Administradores")]
    public class AdministracionEmpleados : Controller
    {
        private readonly ILogger<AdministracionEmpleados> _logger;
        private readonly IReposteriaService _reposteriaService;
        private readonly IMapper _mapper;

        public AdministracionEmpleados(ILogger<AdministracionEmpleados> logger, IReposteriaService reposteriaService, IMapper mapper)
        {
            _logger = logger;
            _reposteriaService = reposteriaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> ListarEmpleados()
        {
            List<EmpleadoResponseDTO>? empleadoListResponseDTO = new List<EmpleadoResponseDTO>();
            APIResponse? response = await _reposteriaService.ObtenerEmpleados<APIResponse>();
            if (response != null)
            {
                empleadoListResponseDTO = JsonConvert.DeserializeObject<List<EmpleadoResponseDTO>>(Convert.ToString(response.Resultado));
            }
            _logger.LogWarning("Lista: " + empleadoListResponseDTO.Count);

            return View(empleadoListResponseDTO);
        }


        //Get
        public async Task<IActionResult> RegistrarEmpleado()
        {
            EmpleadoViewModel empleadoVM = new EmpleadoViewModel();
            var response = await _reposteriaService.ObtenerSucursales<APIResponse>();
            if (response != null)
            {
                empleadoVM.sucursalesDtoList = JsonConvert.DeserializeObject<List<SucursalResponseDTO>>(Convert.ToString(response.Resultado)).
                    Select(suc => new SelectListItem
                    {
                        Text = suc.NombreComercial,
                        Value = suc.IdSucursal.ToString(),
                    });
            }
            return View(empleadoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarEmpleado(EmpleadoViewModel modelo)
        {
            //SE RCOMIENDA VER EL VIDEO 64 Y 65 DE MV PARA HECER LO DE SERVICE Y VALIDACION
            var response = await _reposteriaService.RegistrarEmpleado<APIResponse>(modelo.EmpleadocreateDto);

            if (response != null)
            {
                return RedirectToAction(nameof(ListarEmpleados));
            }
            return View(modelo);
        }
    }
}
