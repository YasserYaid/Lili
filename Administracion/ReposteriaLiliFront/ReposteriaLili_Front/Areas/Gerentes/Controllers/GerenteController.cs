using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Newtonsoft.Json;
using ReposteriaLili_Front.Models;
using ReposteriaLili_Front.Models.DTO;
using ReposteriaLili_Front.Models.DTO.EmpleadosDTOs;
using ReposteriaLili_Front.Models.DTO.OrdenesDTOs;
using ReposteriaLili_Front.Models.DTO.SucursalesDTOs;
using ReposteriaLili_Front.Models.ReqResModels;
using ReposteriaLili_Front.Models.ViewModels;
using ReposteriaLili_Front.Services.IServices;
using System.Diagnostics;

namespace ReposteriaLili_Front.Areas.Gerentes.Controllers
{
    [Area("Gerentes")]
    public class GerenteController : Controller
    {
        private readonly ILogger<GerenteController> _logger;
        private readonly IReposteriaService _reposteriaService;
        private readonly IMapper _mapper;

        public GerenteController(ILogger<GerenteController> logger, IReposteriaService reposteriaService, IMapper mapper)
        {
            _logger = logger;
            _reposteriaService = reposteriaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> ListarPedidosRealizados()
        {
            List<OrdenResponseDTO>? allOrdenListDTO = new List<OrdenResponseDTO>();
            APIResponse? response = await _reposteriaService.ObtenerOrdenes<APIResponse>();///<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            if (response != null)
            {
                allOrdenListDTO = JsonConvert.DeserializeObject<List<OrdenResponseDTO>>(Convert.ToString(response.Resultado));
            }
            return View(allOrdenListDTO);
        }


        public async Task<IActionResult> AsignarRepartidor(int idOrden)
        {            
            OrdenViewModel ordenVM = new OrdenViewModel();
            APIResponse? ordenResponse = await _reposteriaService.ObtenerOrden<APIResponse>(idOrden);
            if (ordenResponse != null)
            {
                OrdenResponseDTO ordenResponseDTO = JsonConvert.DeserializeObject<OrdenResponseDTO>(Convert.ToString(ordenResponse.Resultado));
                OrdenUpdateDTO ordenUpdatedto = _mapper.Map<OrdenUpdateDTO>(ordenResponseDTO);
                ordenVM.OrdenUpdateDTO = ordenUpdatedto;


                APIResponse? repartidoresListResponse = await _reposteriaService.ObtenerRepartidores<APIResponse>();
                if (repartidoresListResponse != null)
                {
                    ordenVM.repartidoresDtoList = JsonConvert.DeserializeObject<List<EmpleadoResponseDTO>>(Convert.ToString(repartidoresListResponse.Resultado)).
                        Select(rep => new SelectListItem
                        {
                            Text = rep.Nombre + " " + rep.ApellidoPaterno + " " + rep.ApellidoMaterno,
                            Value = rep.IdEmpleado.ToString(),
                        });
                    return View(ordenVM);
                }
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarRepartidor(OrdenViewModel modelo)
        {
            var asignnarRepartidorRequest = new AssignDeliveryRequestDTO();
            asignnarRepartidorRequest.IdOrden = modelo.OrdenUpdateDTO.IdOrden;
            asignnarRepartidorRequest.IdRepartidor = (int)modelo.OrdenUpdateDTO.IdEmpleadoRepartidor;
            var response = await _reposteriaService.AsingarRepartidorOrden<APIResponse>(asignnarRepartidorRequest);
            _logger.LogWarning("Repartidor: " + asignnarRepartidorRequest.IdRepartidor + " Orden id: " + asignnarRepartidorRequest.IdOrden.ToString());
            if (response != null)
            {
                return RedirectToAction(nameof(ListarPedidosRealizados));
            }
            return View(modelo);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
