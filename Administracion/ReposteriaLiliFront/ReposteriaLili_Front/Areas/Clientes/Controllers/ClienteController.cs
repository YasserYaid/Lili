using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReposteriaLili_Front.Models;
using ReposteriaLili_Front.Models.DTO.ProductosDTOs;
using ReposteriaLili_Front.Models.ReqResModels;
using ReposteriaLili_Front.Services.IServices;
using System.Diagnostics;

namespace ReposteriaLili_Front.Areas.Clientes.Controllers
{
    [Area("Clientes")]
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IReposteriaService _reposteriaService;
        private readonly IMapper _mapper;

        public ClienteController(ILogger<ClienteController> logger, IReposteriaService reposteriaService, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _reposteriaService = reposteriaService;
        }

        public async Task<IActionResult> ListarProductos() //"INDEX"
        {
            List<ProductoResponseDTO>? inventarioListRespsonseDTO = new List<ProductoResponseDTO>();
            APIResponse? response = await _reposteriaService.ObtenerTodosProductos<APIResponse>();

            if (response != null)
            {
                inventarioListRespsonseDTO = JsonConvert.DeserializeObject<List<ProductoResponseDTO>>(Convert.ToString(response.Resultado));
            }

            return View(inventarioListRespsonseDTO);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
