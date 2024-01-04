using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReposteriaLili_Front.Models.DTO;
using ReposteriaLili_Front.Models.DTO.OrdenesDTOs;
using ReposteriaLili_Front.Models.ReqResModels;
using ReposteriaLili_Front.Services;
using ReposteriaLili_Front.Services.IServices;

namespace ReposteriaLili_Front.Areas.Clientes.Controllers
{
    [Area("Clientes")]
    public class ClientePedidosController : Controller
    {
        private readonly ILogger<ClientePedidosController> _logger;
        private readonly IReposteriaService _reposteriaService;
        private readonly IMapper _mapper;
        ManageImageFirebaseService _manageImageFirebaseService;

        public ClientePedidosController(ILogger<ClientePedidosController> logger, IReposteriaService reposteriaService, IMapper mapper)
        {
            _logger = logger;
            _reposteriaService = reposteriaService;
            _mapper = mapper;
            _manageImageFirebaseService = new ManageImageFirebaseService();
        }

        public async Task<IActionResult> ListarPedidosSolicitados()
        {
            List<CustomerOrderProductResponseDTO>? ordenesClienteListDTO = new List<CustomerOrderProductResponseDTO>();
            APIResponse? response = await _reposteriaService.ObtenerPedidosSolicitadoCliente<APIResponse>(1);///<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            if (response != null)
            {
                ordenesClienteListDTO = JsonConvert.DeserializeObject<List<CustomerOrderProductResponseDTO>>(Convert.ToString(response.Resultado));
            }
            _logger.LogWarning("Elementos de la lista de ordenes del cliente: " + ordenesClienteListDTO.Count);

            return View(ordenesClienteListDTO);
        }

        public async Task<IActionResult> ReportarIncidente(int idOrden)
        {
            APIResponse? apiResponse = await _reposteriaService.ObtenerOrdenClineteInfo<APIResponse>(idOrden);
            if (apiResponse != null)
            {
                CustomerOrderProductResponseDTO customerOrdenResponseDTO = JsonConvert.DeserializeObject<CustomerOrderProductResponseDTO>(Convert.ToString(apiResponse.Resultado));
                return View(customerOrdenResponseDTO);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportarIncidente(CustomerOrderProductResponseDTO modelo)
        {
            var imagefile = HttpContext.Request.Form.Files.Last();
            DateTimeOffset timeNow = (DateTimeOffset)DateTime.UtcNow;
            var nameImageUrl = "Incidencia-Cliente-" + modelo.NumeroFolio + "-" + timeNow.ToUnixTimeMilliseconds().ToString();

            var streamImage = imagefile.OpenReadStream();
            modelo.ImagenIncidenteCliente = await _manageImageFirebaseService.UploadImage(streamImage, nameImageUrl, "INCIDENCIA_CLIENTE");
            OrdenUpdateDTO ordenUpdateDTO = MapearInfo(modelo);
            var response = await _reposteriaService.ActualizarOrden<APIResponse>(ordenUpdateDTO);

            if (response != null)
            {
                return RedirectToAction(nameof(ListarPedidosSolicitados));
            }

            return View(modelo);
        }

        public OrdenUpdateDTO MapearInfo(CustomerOrderProductResponseDTO customerOrderProductResponse)
        {
            return new OrdenUpdateDTO()
            {
                IdOrden = customerOrderProductResponse.IdOrden,
                IdDireccionCliente = customerOrderProductResponse.IdDireccion,
                IdDireccionSucursal = customerOrderProductResponse.IdSucursal,
                IdEmpleadoRepartidor = customerOrderProductResponse.IdEmpleado,
                IdTarjeta = customerOrderProductResponse.IdTarjeta,
                DescripcionIncidenteCliente = customerOrderProductResponse.DescripcionIncidenteCliente,
                DescripcionIncidenteRepartidor = customerOrderProductResponse.DescripcionIncidenteRepartidor,
                ImagenIncidenteCliente = customerOrderProductResponse.ImagenIncidenteCliente,
                ImagenIncidenteRepartidor = customerOrderProductResponse.ImagenIncidenteRepartidor,
                EstadoPedido = customerOrderProductResponse.EstadoPedido,
                FechaEntrega = customerOrderProductResponse.FechaEntrega,
                FechaSolicitud = customerOrderProductResponse.FechaSolicitud,
                ImporteTotalOrden = customerOrderProductResponse.ImporteTotalOrden,
                NumeroFolio = customerOrderProductResponse.NumeroFolio
            };
        }


        public async Task<IActionResult> CancelarEntrega(int idOrden)
        {
            APIResponse? apiResponse = await _reposteriaService.ObtenerOrdenClineteInfo<APIResponse>(idOrden);
            if (apiResponse != null)
            {
                CustomerOrderProductResponseDTO customerOrdenResponseDTO = JsonConvert.DeserializeObject<CustomerOrderProductResponseDTO>(Convert.ToString(apiResponse.Resultado));
                return View(customerOrdenResponseDTO);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelarEntrega(CustomerOrderProductResponseDTO modelo)
        {
            var response = await _reposteriaService.EliminarOrden<APIResponse>(modelo.IdOrden);

            if (response != null)
            {
                return RedirectToAction(nameof(ListarPedidosSolicitados));
            }

            return View(modelo);
        }


    }
}
