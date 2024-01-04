using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Commands.CreateEmployee;
using ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Querys.GetDMList;
using ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Querys.GetEmployeeList;
using ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Querys.Login;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetCOL;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetDMOL;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetOL;
using ReposteriaLili_API.Core.Application.BusinessLogic.SucursalesCQRS.Querys.GetBList;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.DTO.EmpleadosDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using System.Net;

namespace ReposteriaLili_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly ILogger<EmpleadoController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        protected APIResponse _apiRespuesta;
        private readonly string? _secretKey;

        public EmpleadoController(ILogger<EmpleadoController> logger, IUnidadTrabajo unidadTrabajo, IMapper mapper, IMediator mediator, IConfiguration configuration)
        {
            this._logger = logger;
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _mediator = mediator;
            _apiRespuesta = new APIResponse();
            _secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }



        /// CU01 Iniciar sesion empleado
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginEmpleadoRequestDTO loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(loginRequest);
                }

                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new LoginEmployeeQuery(loginRequest, _secretKey));
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



        ///////////////CU_10 REGISTRAR/USUARIOS EMPLEADOS
        [HttpPost("Registrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> RegistrarEmpleado([FromBody] EmpleadoCreateDTO empleadoCreateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(empleadoCreateDTO);
                }

                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new CreateEmployeeCommand(empleadoCreateDTO));
                _apiRespuesta = _mapper.Map<APIResponse>(databaseRespuesta); ///// SE CONVIERTE DABASE_RESPONSE AL API_RESPONSE// SE CONVIERTE LA RESPUESTA DE LA BASE DE DATOS A LA RESPUESTA FINAL A ENVIAR

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



        //CU08 CONSULTAR/LISTAR PEDIDOS ASIGNADOS
        [HttpGet("PedidosAsignados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ConsultarPedidos([FromQuery] int IdEmpleado)
        {
            try
            {
                _apiRespuesta.LimpiarValores();
                if (IdEmpleado <= 0)
                {
                    _apiRespuesta.ControlCode = Constantes.CU08_CODIGO_LISTAR_PEDIDOS_FALLIDO;
                    _apiRespuesta.ControlMessage = Constantes.CU08_MENSAJE_LISTAR_PEDIDOS_FALLIDO;
                    _apiRespuesta.ErrorMessages = new List<string>() { Constantes.CU08_MENSAJE_LISTAR_PEDIDOS_FALLIDO };
                    _apiRespuesta.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiRespuesta);
                }

                DatabaseResponse databaseRespuesta = await _mediator.Send(new GetDeliveryManOrderListQuery(IdEmpleado));
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




        //CU07  FUNCIONALIDAD NECESARIA
        [HttpGet("ListarRepartidores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ObtenerRepartidores()
        {
            try
            {
                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new GetDeliveryManListQuery());
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


        //FUNCIONALIDAD PARA TABLA CU10 REGISTRAR EMPLEADOS
        [HttpGet("ListarEmpleados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ObtenerEmpleados()
        {
            try
            {
                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new GetEmployeeListQuery());
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
