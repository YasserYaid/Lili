using AutoMapper;
using MediatR;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetCOL;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.Internal.Repositorio;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetCO
{
    public class GetCustomerOrderQueryHandler : IRequestHandler<GetCustomerOrderQuery, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public GetCustomerOrderQueryHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(GetCustomerOrderQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();

                OrdenClienteJVM? ordenJVMRecuperada = _unidadTrabajo.viewOrdenesClienteRepo.ObtenerTodos().Result.Where(ordjvm => ordjvm.IdOrden == request.IdOrder).FirstOrDefault();
                
                if (ordenJVMRecuperada == null)
                {
                    _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_NO_SE_ENCONTRARON_ORDENES;
                    _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_NO_SE_ENCONTRARON_ORDENES;
                }
                else
                {
                    CustomerOrderProductResponseDTO customerOrdenesResponseDTO = _mapper.Map<CustomerOrderProductResponseDTO>(ordenJVMRecuperada);

                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_ORDENES_RECUPERADAS_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_ORDENES_RECUPERADAS_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = customerOrdenesResponseDTO;
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
