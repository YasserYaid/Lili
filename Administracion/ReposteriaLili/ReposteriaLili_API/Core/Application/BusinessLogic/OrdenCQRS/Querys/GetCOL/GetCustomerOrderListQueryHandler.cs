using AutoMapper;
using MediatR;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.ClientesDTOs;
using ReposteriaLili_API.Core.Application.DTO.DireccionesDTOs;
using ReposteriaLili_API.Core.Application.DTO.TarjetasDTOs;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.ReqResModels;
using Microsoft.IdentityModel.Tokens;
using ReposteriaLili_API.Core.Application.DTO.ProductosDTOs;
using ReposteriaLili_API.Core.Application.DTO.SucursalesDTOs;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetCOL
{
    public class GetCustomerOrderListQueryHandler : IRequestHandler<GetCustomerOrderListQuery, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;
        private IEnumerable<CustomerOrderProductResponseDTO> customerOrderProductListResponseDTO;

        public GetCustomerOrderListQueryHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(GetCustomerOrderListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<OrdenClienteJVM> ordenesFoundInfo = null;

                _dataBaseResponse.LimpiarValores();

                ordenesFoundInfo = _unidadTrabajo.viewOrdenesClienteRepo.ObtenerTodos().Result.Where(ordjvm => ordjvm.IdCliente == request.IdCliente);

                if (ordenesFoundInfo.IsNullOrEmpty())
                {
                    _dataBaseResponse.ControlCode = Constantes.CU05_CODIGO_NO_SE_ENCONTRARON_PEDIDOS;
                    _dataBaseResponse.ControlMessage = Constantes.CU05_MENSAJE_NO_SE_ENCONTRARON_PEDIDOS;
                }
                else
                {
                    customerOrderProductListResponseDTO = _mapper.Map<IEnumerable<CustomerOrderProductResponseDTO>>(ordenesFoundInfo);
                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU05_CODIGO_ORDENES_RECUPERADAS_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU05_MENSAJE_ORDENES_RECUPERADAS_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = customerOrderProductListResponseDTO;                    
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
