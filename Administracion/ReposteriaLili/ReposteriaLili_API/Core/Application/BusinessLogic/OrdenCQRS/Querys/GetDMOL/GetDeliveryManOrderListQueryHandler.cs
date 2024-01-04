
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;
using ReposteriaLili_API.Infraestructure.Internal.Repositorio;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetDMOL
{
    public class GetDeliveryManOrderListQueryHandler : IRequestHandler<GetDeliveryManOrderListQuery, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;
        private IEnumerable<DeliveryManOrderResponseDTO> deliveryManOrderListResponseDTO;

        public GetDeliveryManOrderListQueryHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(GetDeliveryManOrderListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<OrdenEmpleadoJVM> ordenesFoundInfo = null;

                _dataBaseResponse.LimpiarValores();

                ordenesFoundInfo = _unidadTrabajo.viewOrdenesEmpleadoRepo.ObtenerTodos().Result.Where(ordjvm => ordjvm.IdEmpleado == request.IdEmpleado);

                if (ordenesFoundInfo.IsNullOrEmpty())
                {
                    _dataBaseResponse.ControlCode = Constantes.CU08_CODIGO_NO_SE_ENCONTRARON_PEDIDOS_ASIGNADOS;
                    _dataBaseResponse.ControlMessage = Constantes.CU08_MENSAJE_NO_SE_ENCONTRARON_PEDIDOS_ASIGNADOS;
                }
                else
                {
                    deliveryManOrderListResponseDTO = _mapper.Map<IEnumerable<DeliveryManOrderResponseDTO>>(ordenesFoundInfo);
                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU08_CODIGO_ORDENES_ASIGNADAS_RECUPERADAS_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU08_MENSAJE_ORDENES_ASIGNADAS_RECUPERADAS_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = deliveryManOrderListResponseDTO;
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
