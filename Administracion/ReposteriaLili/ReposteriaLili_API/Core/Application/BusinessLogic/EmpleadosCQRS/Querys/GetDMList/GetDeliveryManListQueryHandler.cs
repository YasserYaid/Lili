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

namespace ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Querys.GetDMList
{
    public class GetDeliveryManListQueryHandler : IRequestHandler<GetDeliveryManListQuery, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public GetDeliveryManListQueryHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(GetDeliveryManListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();

                List<Empleado> repartidoresRecuperados = await _unidadTrabajo.empleadoRepo.ObtenerTodos(emp => emp.Cargo.ToUpper().Equals("REPARTIDOR"));


                if (repartidoresRecuperados.IsNullOrEmpty())
                {
                    _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_NO_SE_ENCONTRARON_REPARTIDORES;
                    _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_NO_SE_ENCONTRARON_REPARTIDORES;
                }
                else
                {
                    IEnumerable<EmpleadoResponseDTO> repartidoresResponseListDTO = _mapper.Map<IEnumerable<EmpleadoResponseDTO>>(repartidoresRecuperados);

                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_REPARTIDORES_RECUPERADOS_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_REPARTIDORES_RECUPERADOS_SATISFATORIAMENTE;
                    _dataBaseResponse.Resultado = repartidoresResponseListDTO;
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
