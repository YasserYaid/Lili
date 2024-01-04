using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReposteriaLili_API.Core.Application.BusinessLogic.ClientesCQRS.Commands.CreateAccount;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Commands.CreateOrder;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Commands.DeleteOrder;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Commands.UpdateOrder;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetOL;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetOrder;
using ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Commands.CreateProduct;
using ReposteriaLili_API.Core.Application.BusinessLogic.SucursalesCQRS.Querys.GetBList;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;
using ReposteriaLili_API.Core.Application.DTO.ProductosDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Core.Dominio.Constantes;
using System.Net;

namespace ReposteriaLili_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly ILogger<OrdenController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        protected APIResponse _apiRespuesta;

        public OrdenController(ILogger<OrdenController> logger, IUnidadTrabajo unidadTrabajo, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _mediator = mediator;
            _apiRespuesta = new APIResponse();
        }



        /// CU04 Registrar Orden
        [HttpPost("Registrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> RegistrarOrden([FromBody] CartOrderCreateDTO cartOrderCreateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(cartOrderCreateDTO);
                }

                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new CreateOrderCommand(cartOrderCreateDTO));
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




        /// CU06 Cancelar orden
        [HttpDelete("Eliminar/{idOrden:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CanecelarOrden(int idOrden)
        {
            try
            {
                if (idOrden <= 0)
                {
                    _apiRespuesta.StatusCode = HttpStatusCode.BadRequest;
                    ModelState.AddModelError("MensajeError", "No se puede eliminar sin un id de orden");
                    return BadRequest(ModelState);
                }

                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new DeleteOrderCommand(idOrden));
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



        /// CU07 Asignar repartidor
        [HttpPut("AsignarRepartidor/{idOrden:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AsignarRepartidor([FromBody] AssignDeliveryRequestDTO assignRequestDTO)
        {
            try
            {
                _logger.LogWarning("Repartidor: " + assignRequestDTO.IdRepartidor + " Orden id: " + assignRequestDTO.IdOrden.ToString());

                if (!ModelState.IsValid)
                {
                    return BadRequest(assignRequestDTO);
                }

                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new AssignOrderDeliveryCommand(assignRequestDTO));
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



        ///CU09 ActualizarOrdenRetornando
        [HttpPut("Actualizar/{idOrden:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> ActualizarOrden([FromBody] OrdenUpdateDTO ordenUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ordenUpdateDTO);
                }


                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new UpdateOrderCommand(ordenUpdateDTO));
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


        //CU07  FUNCIONALIDAD NECESARIA
        [HttpGet("ListarOrdenes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ObtenerOrdene()
        {
            try
            {
                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new GetOrderListQuery());
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



        //FUNCIONALIDAD NECESARIA
        [HttpGet("Detalle/{idOrden:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ObtenerOrden(int idOrden)
        {
            try
            {
                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new GetOrderByIdQuery(idOrden));
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
