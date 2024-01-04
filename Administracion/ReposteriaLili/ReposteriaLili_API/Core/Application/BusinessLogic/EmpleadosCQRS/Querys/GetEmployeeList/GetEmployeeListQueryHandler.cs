using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using ReposteriaLili_API.Core.Application.BusinessLogic.SucursalesCQRS.Querys.GetBList;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.SucursalesDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.Internal.Repositorio;
using ReposteriaLili_API.Core.Application.DTO.EmpleadosDTOs;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Querys.GetEmployeeList
{
    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public GetEmployeeListQueryHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();

                List<Empleado> empleadosRecuperados = await _unidadTrabajo.empleadoRepo.ObtenerTodos();

                if (empleadosRecuperados.Count == 0)
                {
                    _dataBaseResponse.ControlCode = Constantes.CU10_CODIGO_NO_SE_ENCONTRARON_EMPLEADOS;
                    _dataBaseResponse.ControlMessage = Constantes.CU10_MENSAJE_NO_SE_ENCONTRARON_EMPLEADOS;
                }
                else
                {
                    IEnumerable<EmpleadoResponseDTO> empleadosResponseListDTO = _mapper.Map<IEnumerable<EmpleadoResponseDTO>>(empleadosRecuperados);

                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU10_CODIGO_EMPLEADOS_RECUPERADOS_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU10_MENSAJE_EMPLEADOS_RECUPERADOS_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = empleadosResponseListDTO;
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
