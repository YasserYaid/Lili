using AutoMapper;
using MediatR;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Core.Dominio.Constantes;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Commands.UpdateOrder
{
    public class AssignOrderDeliveryCommandHandler : IRequestHandler<AssignOrderDeliveryCommand, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public AssignOrderDeliveryCommandHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(AssignOrderDeliveryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();
                Orden? orderModificada = null;

                if (await _unidadTrabajo.empleadoRepo.Obtener(emp => emp.IdEmpleado == request.assignDeliveryRequest.IdRepartidor) != null)
                {

                    orderModificada = await _unidadTrabajo.ordenRepo.AsignarRepartidorOrden(request.assignDeliveryRequest.IdOrden, request.assignDeliveryRequest.IdRepartidor);

                    if (orderModificada == null)
                    {
                        _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_NO_SE_ENCONTRO_ORDEN;
                        _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_NO_SE_ENCONTRO_ORDEN;
                    }
                    else
                    {
                        OrdenResponseDTO ordenResponseDTO = _mapper.Map<OrdenResponseDTO>(orderModificada);
                        _dataBaseResponse.isExitoso = true;
                        _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_ASIGNACION_REPARTIDOR_SATISFACTORIA;
                        _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_ASIGNACION_REPARTIDOR_SATISFACTORIA;
                        _dataBaseResponse.ErrorMessages = null;
                        _dataBaseResponse.Resultado = ordenResponseDTO;
                    }
                }
                else
                {
                    _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_NO_SE_ENCONTRO_REPARTIDOR;
                    _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_NO_SE_ENCONTRO_REPARTIDOR;
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
