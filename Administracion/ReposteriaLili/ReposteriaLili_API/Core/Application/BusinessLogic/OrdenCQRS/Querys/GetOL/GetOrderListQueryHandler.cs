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
using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetOL
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public GetOrderListQueryHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();

                List<Orden> ordenesRecuperadas = await _unidadTrabajo.ordenRepo.ObtenerTodos();


                if (ordenesRecuperadas.IsNullOrEmpty())
                {
                    _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_NO_SE_ENCONTRARON_ORDENES;
                    _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_NO_SE_ENCONTRARON_ORDENES;
                }
                else
                {
                    IEnumerable<OrdenResponseDTO> ordenesResponseListDTO = _mapper.Map<IEnumerable<OrdenResponseDTO>>(ordenesRecuperadas);

                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_ORDENES_RECUPERADAS_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_ORDENES_RECUPERADAS_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = ordenesResponseListDTO;
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
