using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReposteriaLili_API.Core.Application.BusinessLogic.ClientesCQRS.Commands.CreateAccount;
using ReposteriaLili_API.Core.Application.BusinessLogic.ClientesCQRS.Querys.Login;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetCO;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetCOL;
using ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Querys.GetPL;
using ReposteriaLili_API.Core.Application.BusinessLogic.SucursalesCQRS.Commands.CreateBranch;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.DTO.ClientesDTOs;
using ReposteriaLili_API.Core.Application.DTO.SucursalesDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Core.Dominio.Constantes;
using System.Net;

namespace ReposteriaLili_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        protected APIResponse _apiRespuesta;
        private readonly string? _secretKey;

        public ClienteController(ILogger<ClienteController> logger, IUnidadTrabajo unidadTrabajo, IMapper mapper, IMediator mediator, IConfiguration configuration)
        {
            this._logger = logger;
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _mediator = mediator;
            _apiRespuesta = new APIResponse();
            _secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }


        /// CU01 Iniciar Sesion Cliente
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginClienteRequestDTO loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(loginRequest);
                }

                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new LoginClienteQuery(loginRequest, _secretKey));
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



        /// CU02 Registrar Cliente
        [HttpPost("Registrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> RegistrarClientes([FromBody] AccountCreateDTO accountCreateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(accountCreateDTO);
                }

                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new CreateAccountCommand(accountCreateDTO));
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



        //CU05 CONSULTAR/LISTAR PEDIDOS CLIENTE
        [HttpGet("Pedidos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ConsultarPedidos( [FromQuery] int IdCliente)
        {
            try
            {
                _apiRespuesta.LimpiarValores();
                if (IdCliente <= 0)
                {
                    _apiRespuesta.ControlCode = Constantes.CU05_CODIGO_LISTAR_PEDIDOS_FALLIDO;
                    _apiRespuesta.ControlMessage = Constantes.CU05_MENSAJE_LISTAR_PEDIDOS_FALLIDO;
                    _apiRespuesta.ErrorMessages = new List<string>() { Constantes.CU05_MENSAJE_LISTAR_PEDIDOS_FALLIDO };
                    _apiRespuesta.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiRespuesta);
                }

                DatabaseResponse databaseRespuesta = await _mediator.Send(new GetCustomerOrderListQuery(IdCliente));
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

        [HttpGet("PedidoInfo/{idOrden:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ObtenerUnPedidoC(int idOrden)
        {
            try
            {
                _apiRespuesta.LimpiarValores();
                if (idOrden <= 0)
                {
                    _apiRespuesta.ControlCode = Constantes.CU05_CODIGO_LISTAR_PEDIDOS_FALLIDO;
                    _apiRespuesta.ControlMessage = Constantes.CU05_MENSAJE_LISTAR_PEDIDOS_FALLIDO;
                    _apiRespuesta.ErrorMessages = new List<string>() { Constantes.CU05_MENSAJE_LISTAR_PEDIDOS_FALLIDO };
                    _apiRespuesta.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiRespuesta);
                }

                DatabaseResponse databaseRespuesta = await _mediator.Send(new GetCustomerOrderQuery(idOrden));
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
