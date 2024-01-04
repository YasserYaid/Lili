using AutoMapper;
using BarcodeStandard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ReposteriaLili_Front.Models.DTO;
using ReposteriaLili_Front.Models.DTO.ProductosDTOs;
using ReposteriaLili_Front.Models.DTO.SucursalesDTOs;
using ReposteriaLili_Front.Models.ReqResModels;
using ReposteriaLili_Front.Models.ViewModels;
using ReposteriaLili_Front.Services;
using ReposteriaLili_Front.Services.IServices;
using SkiaSharp;

namespace ReposteriaLili_Front.Areas.Administradores.Controllers
{
    [Area("Administradores")]
    public class AdministracionProductosController : Controller
    {
        private readonly ILogger<AdministracionProductosController> _logger;
        private readonly IReposteriaService _reposteriaService;
        private readonly IMapper _mapper;
        ManageImageFirebaseService _manageImageFirebaseService;

        public AdministracionProductosController(ILogger<AdministracionProductosController> logger, IReposteriaService reposteriaService, IMapper mapper)
        {
            _logger = logger;
            _reposteriaService = reposteriaService;
            _mapper = mapper;
            _manageImageFirebaseService = new ManageImageFirebaseService();
        }
        public async Task<IActionResult> ListarInventario()
        {
            List<ProductoResponseDTO>? inventarioListRespsonseDTO = new List<ProductoResponseDTO>();
            APIResponse? response = await _reposteriaService.ObtenerTodosProductos<APIResponse>();///<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            if (response != null)
            {
                inventarioListRespsonseDTO = JsonConvert.DeserializeObject<List<ProductoResponseDTO>>(Convert.ToString(response.Resultado));
            }
            _logger.LogWarning("Todos los productos son: " + inventarioListRespsonseDTO.Count);

            return View(inventarioListRespsonseDTO);
        }

        //Get
        public async Task<IActionResult> RegistrarProductos()
        {
            ProductoViewModel productoVM = new ProductoViewModel();
            var response = await _reposteriaService.ObtenerSucursales<APIResponse>();
            if (response != null)
            {
                productoVM.sucursalesDtoList = JsonConvert.DeserializeObject<List<SucursalResponseDTO>>(Convert.ToString(response.Resultado)).
                    Select(suc => new SelectListItem
                    {
                        Text = suc.NombreComercial,
                        Value = suc.IdSucursal.ToString(),
                    });
            }
            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarProductos(ProductoViewModel modelo)
        {
            var imagefile = HttpContext.Request.Form.Files.Last();
            //long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            DateTimeOffset timeNow = (DateTimeOffset)DateTime.UtcNow;
            var nameProductUrl = "Product-" + modelo.productoCreateDto.Nombre + timeNow.ToUnixTimeMilliseconds().ToString();

            modelo.productoCreateDto.CodigoBarras = modelo.productoCreateDto.Nombre + timeNow.ToUnixTimeMilliseconds().ToString();
            var nameBarcodeUrl = "Barcode-" + modelo.productoCreateDto.CodigoBarras;

            var streamImage = imagefile.OpenReadStream();
            modelo.productoCreateDto.ImagenUrl = await _manageImageFirebaseService.UploadImage(streamImage, nameProductUrl, "PRODUCT");

            Barcode barcodeData = new Barcode();
            barcodeData.IncludeLabel = true;
            barcodeData.Alignment = AlignmentPositions.Center;
            var barcodeDataImage = barcodeData.Encode(BarcodeStandard.Type.Code128, modelo.productoCreateDto.CodigoBarras, 1200, 600);

            var barcodeDataStream = barcodeDataImage.Encode(SKEncodedImageFormat.Png, 100).AsStream();
            modelo.productoCreateDto.ImagenCodigoBarrasUrl = await _manageImageFirebaseService.UploadImage(barcodeDataStream, nameBarcodeUrl, "BARCODE");
            _logger.LogWarning(modelo.productoCreateDto.ImagenCodigoBarrasUrl);


            var response = await _reposteriaService.RegistrarProducto<APIResponse>(modelo.productoCreateDto);

            if (response != null)
            {
                return RedirectToAction(nameof(ListarInventario));
            }
            return View(modelo);
        }
    }
}
