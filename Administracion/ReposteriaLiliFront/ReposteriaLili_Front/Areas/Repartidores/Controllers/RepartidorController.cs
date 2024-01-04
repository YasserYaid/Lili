using AutoMapper;
using BarcodeStandard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Newtonsoft.Json;
using ReposteriaLili_Front.Models;
using ReposteriaLili_Front.Models.DTO;
using ReposteriaLili_Front.Models.DTO.EmpleadosDTOs;
using ReposteriaLili_Front.Models.DTO.OrdenesDTOs;
using ReposteriaLili_Front.Models.ReqResModels;
using ReposteriaLili_Front.Models.ViewModels;
using ReposteriaLili_Front.Services;
using ReposteriaLili_Front.Services.IServices;
using SkiaSharp;
using System.Diagnostics;

namespace ReposteriaLili_Front.Areas.Repartidores.Controllers
{
    [Area("Repartidores")]
    public class RepartidorController : Controller
    {
        private readonly ILogger<RepartidorController> _logger;
        private readonly IReposteriaService _reposteriaService;
        private readonly IMapper _mapper;
        ManageImageFirebaseService _manageImageFirebaseService;

        public RepartidorController(ILogger<RepartidorController> logger, IReposteriaService reposteriaService, IMapper mapper)
        {
            _logger = logger;
            _reposteriaService = reposteriaService;
            _mapper = mapper;
            _manageImageFirebaseService = new ManageImageFirebaseService();
        }


        public async Task<IActionResult> ListarPedidosAsignados()
        {
            List<DeliveryManOrderResponseDTO>? repartidorOrdenListDTO = new List<DeliveryManOrderResponseDTO>();
            APIResponse? response = await _reposteriaService.ObtenerPedidosAsignados<APIResponse>(1);///<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            if (response != null)
            {
                repartidorOrdenListDTO = JsonConvert.DeserializeObject<List<DeliveryManOrderResponseDTO>>(Convert.ToString(response.Resultado));
            }

            return View(repartidorOrdenListDTO);
        }


        public async Task<IActionResult> ReportarPedido(int idOrden)
        {
            APIResponse? ordenResponse = await _reposteriaService.ObtenerOrden<APIResponse>(idOrden);
            if (ordenResponse != null)
            {
                OrdenResponseDTO ordenResponseDTO = JsonConvert.DeserializeObject<OrdenResponseDTO>(Convert.ToString(ordenResponse.Resultado));
                OrdenUpdateDTO ordenUpdatedto = _mapper.Map<OrdenUpdateDTO>(ordenResponseDTO);
                return View(ordenUpdatedto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportarPedido(OrdenUpdateDTO modelo)
        {
            modelo.EstadoPedido = "NO ENTREGADO";
            modelo.EsOrdenEntregada = false;
            var imagefile = HttpContext.Request.Form.Files.Last();

            DateTimeOffset timeNow = (DateTimeOffset)DateTime.UtcNow;
            var nameImageUrl= "Incidencia-Repartidor-" + modelo.NumeroFolio + "-" + timeNow.ToUnixTimeMilliseconds().ToString();

            var streamImage = imagefile.OpenReadStream();
            modelo.ImagenIncidenteRepartidor = await _manageImageFirebaseService.UploadImage(streamImage, nameImageUrl, "INCIDENCIA_REPARTIDOR");


            var response = await _reposteriaService.ActualizarOrden<APIResponse>(modelo);
            
            if (response != null)
            {
                return RedirectToAction(nameof(ListarPedidosAsignados));
            }

            return View(modelo);
        }



        public async Task<IActionResult> ConfirmarEntrega(int idOrden)
        {
            APIResponse? ordenResponse = await _reposteriaService.ObtenerOrden<APIResponse>(idOrden);
            if (ordenResponse != null)
            {
                OrdenResponseDTO ordenResponseDTO = JsonConvert.DeserializeObject<OrdenResponseDTO>(Convert.ToString(ordenResponse.Resultado));
                OrdenUpdateDTO ordenUpdatedto = _mapper.Map<OrdenUpdateDTO>(ordenResponseDTO);
                return View(ordenUpdatedto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEntrega(OrdenUpdateDTO modelo)
        {
            modelo.EstadoPedido = "ENTREGADO";
            modelo.EsOrdenEntregada = true;
            modelo.ImagenIncidenteRepartidor = null;
            modelo.DescripcionIncidenteRepartidor = null;

            var response = await _reposteriaService.ActualizarOrden<APIResponse>(modelo);

            if (response != null)
            {
                return RedirectToAction(nameof(ListarPedidosAsignados));
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
