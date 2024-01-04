using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReposteriaLili_Front.Areas.Clientes.Controllers;
using ReposteriaLili_Front.Models;
using ReposteriaLili_Front.Models.Dominio;
using ReposteriaLili_Front.Models.DTO.SucursalesDTOs;
using ReposteriaLili_Front.Models.ReqResModels;
using ReposteriaLili_Front.Models.ViewModels;
using ReposteriaLili_Front.Services.IServices;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace ReposteriaLili_Front.Areas.Administradores.Controllers
{
    [Area("Administradores")]
    public class AdministradorController : Controller
    {
        private readonly ILogger<AdministradorController> _logger;
        private readonly IReposteriaService _reposteriaService;
        private readonly IMapper _mapper;
        private List<SucursalViewModel>? ListaSucursalesVM { get; set; }

        public AdministradorController(ILogger<AdministradorController> logger, IReposteriaService reposteriaService, IMapper mapper)
        {
            _logger = logger;
            _reposteriaService = reposteriaService;
            _mapper = mapper;
        }

        public IActionResult ListarSucursales()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SucursalUpSert(int? id)
        {
            SucursalViewModel? sucursal;
            if(id == null)
            {
                //RegistrarSucursal nueva sucursal
                sucursal = new SucursalViewModel();
                _logger.LogInformation("Se creara Nueva sucursal: ");
                return View(sucursal);
            }
            sucursal = BuscarSucursal(id);
            if(sucursal == null)
            {
                _logger.LogInformation("No se encontro la sucursal: ");
                return NotFound();
            }
            _logger.LogInformation("La sucursal : " + sucursal.NombreComercial);
            return View(sucursal);
        }

        #region Metodos HTTP

        public SucursalViewModel? BuscarSucursal(int? id)
        {
            SucursalViewModel? sucursalFound = null;
            if(ListaSucursalesVM.Exists(suc => suc.IdSucursal == id))
            {
                sucursalFound = ListaSucursalesVM.FirstOrDefault(suc => suc.IdSucursal == id);
            }
            return sucursalFound;
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerSucursales()
        {
            List<SucursalResponseDTO>? sucursalListResponseDTO = new List<SucursalResponseDTO>();
            APIResponse? response = await _reposteriaService.ObtenerSucursales<APIResponse>();
            if (response != null)
            {
                sucursalListResponseDTO = JsonConvert.DeserializeObject<List<SucursalResponseDTO>>(Convert.ToString(response.Resultado));
            }
            ListaSucursalesVM = _mapper.Map<List<SucursalViewModel>>(sucursalListResponseDTO);
            return Json(new { data = ListaSucursalesVM });
        }

        [HttpPost]
        public async Task<IActionResult> SucursalUpSert(SucursalViewModel sucursalVM)
        {
            if (ModelState.IsValid)
            {
                SucursalCreateDTO modelo = _mapper.Map<SucursalCreateDTO>(sucursalVM);
                var response = await _reposteriaService.RegistrarSucursal<APIResponse>(modelo);
                _logger.LogInformation(response.ControlMessage);              
            }
            return RedirectToAction("ListarSucursales");
        }

        #endregion
    }
}
