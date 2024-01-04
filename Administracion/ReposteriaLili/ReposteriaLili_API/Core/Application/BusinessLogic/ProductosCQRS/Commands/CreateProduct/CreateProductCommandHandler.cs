using AutoMapper;
using BarcodeStandard;
using MediatR;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.ProductosDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Infraestructure.External.ImageFirebase;
using SkiaSharp;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;
        private ManageImageFirebaseService _manageImageFirebaseService;

        public CreateProductCommandHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
            _manageImageFirebaseService = new ManageImageFirebaseService();
        }

        public async Task<DatabaseResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();

                Producto productoARegistrar = _mapper.Map<Producto>(request.productoCreateDTO);
                productoARegistrar.CantidadDisponible = request.productoCreateDTO.Cantidad;
                productoARegistrar.FechaCaducidad = DateTime.Now.AddDays(7);/////////PARA FECHA DE CADUCIDAD AUTOMATICA
                productoARegistrar = await _unidadTrabajo.productoRepo.RegistrarRetornandoConID(productoARegistrar);


                if (productoARegistrar.IdProducto > 0)
                {
                    Producto_Sucursal producto_sucursal_db_table = new Producto_Sucursal();
                    producto_sucursal_db_table.IdSucursal = request.productoCreateDTO.IdSucursal;
                    producto_sucursal_db_table.Cantidad = request.productoCreateDTO.Cantidad;
                    producto_sucursal_db_table.IdProducto = productoARegistrar.IdProducto;

                    await _unidadTrabajo.producto_Sucursal_Repo.Registrar(producto_sucursal_db_table);

                    ProductoResponseDTO productoResponseDTO = _mapper.Map<ProductoResponseDTO>(productoARegistrar);

                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU12_CODIGO_PRODUCTO_REGISTRADO_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU12_MENSAJE_PRODUCTO_REGISTRADO_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = productoResponseDTO;
                }
                else
                {
                    _dataBaseResponse.ControlCode = Constantes.CU04_CODIGO_REGISTRO_ORDEN_FALLIDO;
                    _dataBaseResponse.ControlMessage = Constantes.CU04_MENSAJE_REGISTRO_ORDEN_FALLIDO;
                }

                return _dataBaseResponse;
            }
            catch (Exception ex)
            {
                _dataBaseResponse.isExitoso = false;
                _dataBaseResponse.ControlCode = Constantes.CODIGO_DATA_BASE_ERROR;
                _dataBaseResponse.ControlMessage = Constantes.MENSAJE_DATA_BASE_ERROR;
                _dataBaseResponse.ErrorMessages = new List<string>() { ex.ToString() };
                _dataBaseResponse.Resultado = null;
                return _dataBaseResponse;
            }
        }
    }
}



/*
         public async Task<DatabaseResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();
                if (request.productoCreateDTO.ImageFile == null)
                {
                    _dataBaseResponse.ControlCode = Constantes.CU12_CODIGO_NO_HAY_IMAGEN;
                    _dataBaseResponse.ControlMessage = Constantes.CU12_MENSAJE_NO_HAY_IMAGEN;
                }
                else
                {
                    //long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                    DateTimeOffset timeNow = (DateTimeOffset)DateTime.UtcNow;
                    var nameProductUrl = "Product-" + request.productoCreateDTO.Nombre + timeNow.ToUnixTimeMilliseconds().ToString();

                    request.productoCreateDTO.CodigoBarras = request.productoCreateDTO.Nombre + timeNow.ToUnixTimeMilliseconds().ToString();
                    var nameBarcodeUrl = "Barcode-" + request.productoCreateDTO.CodigoBarras;

                    var streamImage = request.productoCreateDTO.ImageFile.OpenReadStream();
                    request.productoCreateDTO.ImagenUrl = await _manageImageFirebaseService.UploadImage(streamImage, nameProductUrl, "PRODUCT");

                    Barcode barcodeData = new Barcode();
                    barcodeData.IncludeLabel = true;
                    barcodeData.Alignment = AlignmentPositions.Center;
                    var barcodeDataImage = barcodeData.Encode(BarcodeStandard.Type.Code128, request.productoCreateDTO.CodigoBarras, 1200, 600);

                    var barcodeDataStream = barcodeDataImage.Encode(SKEncodedImageFormat.Png, 100).AsStream();
                    request.productoCreateDTO.ImagenCodigoBarrasUrl = await _manageImageFirebaseService.UploadImage(barcodeDataStream, nameBarcodeUrl, "BARCODE");

                    Producto productoARegistrar = _mapper.Map<Producto>(request.productoCreateDTO);
                    productoARegistrar.CantidadDisponible = request.productoCreateDTO.Cantidad;
                    //                    productoARegistrar.FechaCaducidad = DateTime.Now.AddDays(7);/////////PARA FECHA DE CADUCIDAD AUTOMATICA
                    productoARegistrar = await _unidadTrabajo.productoRepo.RegistrarRetornandoConID(productoARegistrar);


                    if (productoARegistrar.IdProducto > 0)
                    {
                        Producto_Sucursal producto_sucursal_db_table = new Producto_Sucursal();
                        producto_sucursal_db_table.IdSucursal = request.productoCreateDTO.IdSucursal;
                        producto_sucursal_db_table.Cantidad = request.productoCreateDTO.Cantidad;
                        producto_sucursal_db_table.IdProducto = productoARegistrar.IdProducto;

                        await _unidadTrabajo.producto_Sucursal_Repo.Registrar(producto_sucursal_db_table);

                        ProductoResponseDTO productoResponseDTO = _mapper.Map<ProductoResponseDTO>(productoARegistrar);

                        _dataBaseResponse.isExitoso = true;
                        _dataBaseResponse.ControlCode = Constantes.CU12_CODIGO_PRODUCTO_REGISTRADO_SATISFATORIAMENTE;
                        _dataBaseResponse.ControlMessage = Constantes.CU12_MENSAJE_PRODUCTO_REGISTRADO_SATISFATORIAMENTE;
                        _dataBaseResponse.ErrorMessages = null;
                        _dataBaseResponse.Resultado = productoResponseDTO;
                    }
                    else
                    {
                        _dataBaseResponse.ControlCode = Constantes.CU04_CODIGO_REGISTRO_ORDEN_FALLIDO;
                        _dataBaseResponse.ControlMessage = Constantes.CU04_MENSAJE_REGISTRO_ORDEN_FALLIDO;
                    }

                }
                return _dataBaseResponse;
            }
            catch (Exception ex)
            {
                _dataBaseResponse.isExitoso = false;
                _dataBaseResponse.ControlCode = Constantes.CODIGO_DATA_BASE_ERROR;
                _dataBaseResponse.ControlMessage = Constantes.MENSAJE_DATA_BASE_ERROR;
                _dataBaseResponse.ErrorMessages = new List<string>() { ex.ToString() };
                _dataBaseResponse.Resultado = null;
                return _dataBaseResponse;
            }
        }
 */