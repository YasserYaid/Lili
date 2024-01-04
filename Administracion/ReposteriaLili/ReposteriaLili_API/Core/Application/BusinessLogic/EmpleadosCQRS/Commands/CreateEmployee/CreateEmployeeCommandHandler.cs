using AutoMapper;
using MediatR;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.EmpleadosDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Core.Dominio.Constantes;
using System.Net;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public CreateEmployeeCommandHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();
                if (await _unidadTrabajo.empleadoRepo.Obtener(em => em.UserName.ToLower() == request.CreateEmpleadoDTO.UserName.ToLower()) != null)
                {
                    _dataBaseResponse.ControlCode = Constantes.CU02_CU10_CODIGO_USERNAME_REPETIDO;
                    _dataBaseResponse.ControlMessage = Constantes.CU02_CU10_MENSAJE_USERNAME_REPETIDO;
                }
                else if(await _unidadTrabajo.empleadoRepo.Obtener(em => em.Telefono.ToLower() == request.CreateEmpleadoDTO.Telefono.ToLower()) != null)
                {
                    _dataBaseResponse.ControlCode = Constantes.CU02_CU10_CODIGO_TELEFONO_REPETIDO;
                    _dataBaseResponse.ControlMessage = Constantes.CU02_CU10_MENSAJE_TELEFONO_REPETIDO;
                }
                else if(await _unidadTrabajo.empleadoRepo.Obtener(em => em.Correo.ToLower() == request.CreateEmpleadoDTO.Correo.ToLower()) != null)
                {
                    _dataBaseResponse.ControlCode = Constantes.CU02_CU10_CODIGO_CORREO_REPETIDO;
                    _dataBaseResponse.ControlMessage = Constantes.CU02_CU10_MENSAJE_CORREO_REPETIDO;
                }
                else
                {
                    Empleado empleado = _mapper.Map<Empleado>(request.CreateEmpleadoDTO);
                    empleado.FechaIngreso = DateTime.Now;
                    await _unidadTrabajo.empleadoRepo.Registrar(empleado);
                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU10_CODIGO_EMPLEADO_REGISTRADO_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU10_MENSAJE_EMPLEADO_REGISTRADO_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = empleado;
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
