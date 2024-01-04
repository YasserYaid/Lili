using AutoMapper;
using BarcodeStandard;
using MediatR;
using ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Commands.CreateProduct;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.ProductosDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.External.ImageFirebase;
using ReposteriaLili_API.Infraestructure.Internal.Repositorio;
using SkiaSharp;
using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;
using Microsoft.IdentityModel.Tokens;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;
        private ManageImageFirebaseService _manageImageFirebaseService;

        public UpdateOrderCommandHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
            _manageImageFirebaseService = new ManageImageFirebaseService();
        }

        public async Task<DatabaseResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();

                Orden? ordenAActualizar = null;
                bool esOrdenActualizada = false;

                ordenAActualizar = _mapper.Map<Orden>(request.UpdateOrdenDTO);
                if (request.UpdateOrdenDTO.EsOrdenEntregada)
                {
                    ordenAActualizar.TiempoTardoEntrega = DateTime.Now.TimeOfDay;
                    ordenAActualizar.FechaEntrega = DateTime.Now;
                }
                esOrdenActualizada = await _unidadTrabajo.ordenRepo.ActualizarOrden(ordenAActualizar);

                if (esOrdenActualizada)
                {
                    OrdenResponseDTO ordenResponseDTO = _mapper.Map<OrdenResponseDTO>(ordenAActualizar);
                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU09_CODIGO_ORDEN_ACTUALIZADA_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU09_MENSAJE__ORDEN_ACTUALIZADA_SATISFATORIAMENTE;
                    _dataBaseResponse.Resultado = ordenResponseDTO;
                }
                else
                {
                    _dataBaseResponse.ControlCode = Constantes.CU09_CODIGO_NO_SE_ACTUALIZO_LA_ORDEN;
                    _dataBaseResponse.ControlMessage = Constantes.CU09_MENSAJE_NO_SE_ACTUALIZO_LA_ORDEN;
                    _dataBaseResponse.ErrorMessages = new List<string>() { Constantes.CU09_MENSAJE_NO_SE_ACTUALIZO_LA_ORDEN };
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

     try
            {
                _dataBaseResponse.LimpiarValores();

                bool esImagenSubida = true;
                Orden? ordenAActualizar = null;
                bool esOrdenActualizada = false;

                if (request.UpdateOrdenDTO.ImageFile != null)
                {
                    DateTimeOffset timeNow = (DateTimeOffset)DateTime.UtcNow;

                    var nameFileImage = "Incidencia-" + request.UpdateOrdenDTO.UserName + timeNow.ToUnixTimeMilliseconds().ToString();

                    var streamImage = request.UpdateOrdenDTO.ImageFile.OpenReadStream();

                    if (request.UpdateOrdenDTO.DescripcionIncidenteCliente != null)
                    {
                        request.UpdateOrdenDTO.ImagenIncidenteCliente = await _manageImageFirebaseService.UploadImage(streamImage, nameFileImage, "INCIDENCIA_CLIENTE");

                        if (request.UpdateOrdenDTO.ImagenIncidenteCliente.IsNullOrEmpty()) esImagenSubida = false;                        

                    }
                    else if (request.UpdateOrdenDTO.DescripcionIncidenteRepartidor != null)
                    {
                        request.UpdateOrdenDTO.ImagenIncidenteRepartidor = await _manageImageFirebaseService.UploadImage(streamImage, nameFileImage, "INCIDENCIA_REPARTIDOR");

                        if (request.UpdateOrdenDTO.ImagenIncidenteRepartidor.IsNullOrEmpty()) esImagenSubida = false;
                        
                    }

                }

                if (esImagenSubida)
                {
                    ordenAActualizar = _mapper.Map<Orden>(request.UpdateOrdenDTO);
                    esOrdenActualizada = await _unidadTrabajo.ordenRepo.ActualizarOrden(ordenAActualizar);

                    if (esOrdenActualizada)
                    {
                        OrdenResponseDTO ordenResponseDTO = _mapper.Map<OrdenResponseDTO>(ordenAActualizar);
                        _dataBaseResponse.isExitoso = true;
                        _dataBaseResponse.ControlCode = Constantes.CU09_CODIGO_ORDEN_ACTUALIZADA_SATISFATORIAMENTE;
                        _dataBaseResponse.ControlMessage = Constantes.CU09_MENSAJE__ORDEN_ACTUALIZADA_SATISFATORIAMENTE;
                        _dataBaseResponse.Resultado = ordenResponseDTO;
                    }
                    else
                    {
                        _dataBaseResponse.ControlCode = Constantes.CU09_CODIGO_NO_SE_ACTUALIZO_LA_ORDEN;
                        _dataBaseResponse.ControlMessage = Constantes.CU09_MENSAJE_NO_SE_ACTUALIZO_LA_ORDEN;
                        _dataBaseResponse.ErrorMessages = new List<string>() { Constantes.CU09_MENSAJE_NO_SE_ACTUALIZO_LA_ORDEN };
                    }
                }
                else
                {
                    _dataBaseResponse.ControlCode = Constantes.CU09_CODIGO_NO_SE_SUBIERON_LAS_IMAGENES;
                    _dataBaseResponse.ControlMessage = Constantes.CU09_MENSAJE_NO_SE_SUBIERON_LAS_IMAGENES;
                    _dataBaseResponse.ErrorMessages = new List<string>() { Constantes.CU09_MENSAJE_NO_SE_SUBIERON_LAS_IMAGENES};                    
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

*/