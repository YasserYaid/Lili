using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Commands.CreateEmployee;
using ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Querys.GetPL;
using ReposteriaLili_API.Core.Application.BusinessLogic.SucursalesCQRS.Commands.CreateBranch;
using ReposteriaLili_API.Core.Application.BusinessLogic.SucursalesCQRS.Querys.GetBList;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.SucursalesDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Core.Dominio.Constantes;
using System.Net;

namespace ReposteriaLili_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {        
        private readonly ILogger<SucursalController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        protected APIResponse _apiRespuesta;
       
        public SucursalController(ILogger<SucursalController> logger, IUnidadTrabajo unidadTrabajo, IMapper mapper, IMediator mediator)
        {
            this._logger = logger;
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _mediator = mediator;
            _apiRespuesta = new APIResponse();
        }


        /// CU11 Registrar Sucursales
        [HttpPost("Registrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> RegistrarSucursales([FromBody] SucursalCreateDTO sucursalCreateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(sucursalCreateDTO);
                }

                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new CreateBranchCommand(sucursalCreateDTO));
                _apiRespuesta = _mapper.Map<APIResponse>(databaseRespuesta); ///// SE CONVIERTE DABASE_RESPONSE AL API_RESPONSE

                if (_apiRespuesta.isExitoso)
                {
                    _apiRespuesta.StatusCode = HttpStatusCode.OK;
                    return Ok(_apiRespuesta);
                }
                else
                {
                    if (_apiRespuesta.ControlCode == Constantes.CU11_CODIGO_NOMBRE_COMERCIAL_REPETIDO)
                    {
                        _apiRespuesta.StatusCode = HttpStatusCode.BadRequest;
                        ModelState.AddModelError("MensajeError", _apiRespuesta.ControlMessage);
                        return BadRequest(ModelState);
                    }
                    else if (_apiRespuesta.ControlCode == Constantes.CODIGO_DATA_BASE_ERROR)
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
        [HttpGet("ListarSucursales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ObtenerSucursales()
        {
            try
            {
                _apiRespuesta.LimpiarValores();

                DatabaseResponse databaseRespuesta = await _mediator.Send(new GetBranchListQuery());
                _apiRespuesta = _mapper.Map<APIResponse>(databaseRespuesta);

                if (_apiRespuesta.isExitoso)
                {
                    _apiRespuesta.StatusCode = HttpStatusCode.OK;
                    return Ok(_apiRespuesta);
                }
                else
                {
                    if (_apiRespuesta.ControlCode == Constantes.CU07_CODIGO_NO_SE_ENCONTRARON_SUCURSALES)
                    {
                        _apiRespuesta.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(_apiRespuesta);
                    }
                    else if (_apiRespuesta.ControlCode == Constantes.CODIGO_DATA_BASE_ERROR)
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
