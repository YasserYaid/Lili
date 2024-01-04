using AutoMapper;
using Azure;
using Azure.Core;
using BarcodeStandard;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReposteriaLili_API.Core.Application.BusinessLogic.ClientesCQRS.Querys.Login;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetOL;
using ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Commands.CreateProduct;
using ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Querys.GetAllInventory;
using ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Querys.GetPL;
using ReposteriaLili_API.Core.Application.BusinessLogic.SucursalesCQRS.Commands.CreateBranch;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.EmpleadosDTOs;
using ReposteriaLili_API.Core.Application.DTO.ProductosDTOs;
using ReposteriaLili_API.Core.Application.DTO.SucursalesDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Infraestructure.External.ImageFirebase;
using ReposteriaLili_API.Infraestructure.Internal.Repositorio;
using SkiaSharp;
using System.Net;

namespace ReposteriaLili_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        protected APIResponse _apiRespuesta;
        private ManageImageFirebaseService _manageImageFirebaseService;

        public ProductoController(ILogger<ProductoController> logger, IUnidadTrabajo unidadTrabajo, IMapper mapper, IMediator mediator)
        {
            this._logger = logger;
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _mediator = mediator;
            _apiRespuesta = new APIResponse();
        }

        //CU03 CONSULTAR/LISTAR PRODUCTOS
        [HttpGet("Catalogo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ConsultarProductos( [FromQuery] int IdSucursal, [FromQuery] string categoria, [FromQuery] string nombre)
        {
            try
            {
                _apiRespuesta.LimpiarValores();
                if (IdSucursal <= 0)
                {
                    _apiRespuesta.ControlCode = Constantes.CU03_CODIGO_LISTAR_PRODUCTOS_FALLIDO;
                    _apiRespuesta.ControlMessage = Constantes.CU03_MENSAJE_LISTAR_PRODUCTOS_FALLIDO;
                    _apiRespuesta.ErrorMessages = new List<string>() { Constantes.CU03_MENSAJE_LISTAR_PRODUCTOS_FALLIDO };
                    _apiRespuesta.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiRespuesta);
                }

                DatabaseResponse databaseRespuesta = await _mediator.Send(new GetProductListQuery(IdSucursal, categoria, nombre));
                _apiRespuesta = _mapper.Map<APIResponse>(databaseRespuesta);

                if (_apiRespuesta.isExitoso)
                {
                    _apiRespuesta.StatusCode = HttpStatusCode.OK;
                    return Ok(_apiRespuesta);
                }
                else
                {
                    if (_apiRespuesta.ControlCode >= 4000 && _apiRespuesta.ControlCode < 5000)
                    {
                        _apiRespuesta.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(_apiRespuesta);
                    }
                    else if (_apiRespuesta.ControlCode >= 5000 && _apiRespuesta.ControlCode < 6000)
                    {
                        _apiRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                        return StatusCode(StatusCodes.Status500InternalServerError, _apiRespuesta);
                    }
                    else
                    {
                        _apiRespuesta.isExitoso = false;
                        _apiRespuesta.ControlCode = Constantes.CODIGO_NO_HAY_CODIGO_CONTROL_EN_CQRS_HANDLER;
                        _apiRespuesta.ControlMessage = Constantes.MENSAJE_NO_HAY_CODIGO_CONTROL_EN_CQRS_HANDLER;
                        _apiRespuesta.ErrorMessages = new List<string>() { Constantes.MENSAJE_NO_HAY_CODIGO_CONTROL_EN_CQRS_HANDLER };
                        _apiRespuesta.Resultado = null;
                        _apiRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                        return StatusCode(StatusCodes.Status500InternalServerError, _apiRespuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                _apiRespuesta.isExitoso = false;
                _apiRespuesta.ControlCode = Constantes.CODIGO_MEDIATOR_ERROR;
                _apiRespuesta.ControlMessage = Constantes.MENSAJE_MEDIATOR_ERROR;
                _apiRespuesta.ErrorMessages = new List<string>() { ex.ToString() };
                _apiRespuesta.Resultado = null;
                _apiRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _apiRespuesta);
            }
        }




        ///CU12 Registrar Productos
        [HttpPost("Registrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> RegistrarProductos([FromBody] ProductoCreateDTO productoCreateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(productoCreateDTO);
                }

                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new CreateProductCommand(productoCreateDTO));
                _apiRespuesta = _mapper.Map<APIResponse>(databaseRespuesta); ///// SE CONVIERTE DABASE_RESPONSE AL API_RESPONSE

                if (_apiRespuesta.isExitoso)
                {
                    _apiRespuesta.StatusCode = HttpStatusCode.OK;
                    return Ok(_apiRespuesta);
                }
                else
                {
                    if (_apiRespuesta.ControlCode >= 4000 && _apiRespuesta.ControlCode < 5000)
                    {
                        _apiRespuesta.StatusCode = HttpStatusCode.BadRequest;
                        ModelState.AddModelError("MensajeError", _apiRespuesta.ControlMessage);
                        return BadRequest(ModelState);
                    }
                    else if (_apiRespuesta.ControlCode >= 5000 && _apiRespuesta.ControlCode < 6000)
                    {
                        _apiRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                        return StatusCode(StatusCodes.Status500InternalServerError, _apiRespuesta);
                    }
                    else
                    {
                        _apiRespuesta.isExitoso = false;
                        _apiRespuesta.ControlCode = Constantes.CODIGO_NO_HAY_CODIGO_CONTROL_EN_CQRS_HANDLER;
                        _apiRespuesta.ControlMessage = Constantes.MENSAJE_NO_HAY_CODIGO_CONTROL_EN_CQRS_HANDLER;
                        _apiRespuesta.ErrorMessages = new List<string>() { Constantes.MENSAJE_NO_HAY_CODIGO_CONTROL_EN_CQRS_HANDLER };
                        _apiRespuesta.Resultado = null;
                        _apiRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                        return StatusCode(StatusCodes.Status500InternalServerError, _apiRespuesta);
                    }
                }


            }
            catch (Exception ex)
            {
                _apiRespuesta.isExitoso = false;
                _apiRespuesta.ControlCode = Constantes.CODIGO_MEDIATOR_ERROR;
                _apiRespuesta.ControlMessage = Constantes.MENSAJE_MEDIATOR_ERROR;
                _apiRespuesta.ErrorMessages = new List<string>() { ex.ToString() };
                _apiRespuesta.Resultado = null;
                _apiRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _apiRespuesta);
            }
        }



        //CU12  FUNCIONALIDAD NECESARIA
        [HttpGet("ListarInventario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ObtenerInventario()
        {
            try
            {
                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new GetInventoryListQuery());
                _apiRespuesta = _mapper.Map<APIResponse>(databaseRespuesta);

                if (_apiRespuesta.isExitoso)
                {
                    _apiRespuesta.StatusCode = HttpStatusCode.OK;
                    return Ok(_apiRespuesta);
                }
                else
                {
                    if (_apiRespuesta.ControlCode >= 4000 && _apiRespuesta.ControlCode < 5000)
                    {
                        _apiRespuesta.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(_apiRespuesta);
                    }
                    else if (_apiRespuesta.ControlCode >= 5000 && _apiRespuesta.ControlCode < 6000)
                    {
                        _apiRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                        return StatusCode(StatusCodes.Status500InternalServerError, _apiRespuesta);
                    }
                    else
                    {
                        _apiRespuesta.isExitoso = false;
                        _apiRespuesta.ControlCode = Constantes.CODIGO_NO_HAY_CODIGO_CONTROL_EN_CQRS_HANDLER;
                        _apiRespuesta.ControlMessage = Constantes.MENSAJE_NO_HAY_CODIGO_CONTROL_EN_CQRS_HANDLER;
                        _apiRespuesta.ErrorMessages = new List<string>() { Constantes.MENSAJE_NO_HAY_CODIGO_CONTROL_EN_CQRS_HANDLER };
                        _apiRespuesta.Resultado = null;
                        _apiRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                        return StatusCode(StatusCodes.Status500InternalServerError, _apiRespuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                _apiRespuesta.isExitoso = false;
                _apiRespuesta.ControlCode = Constantes.CODIGO_MEDIATOR_ERROR;
                _apiRespuesta.ControlMessage = Constantes.MENSAJE_MEDIATOR_ERROR;
                _apiRespuesta.ErrorMessages = new List<string>() { ex.ToString() };
                _apiRespuesta.Resultado = null;
                _apiRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _apiRespuesta);
            }
        }

    }
}
