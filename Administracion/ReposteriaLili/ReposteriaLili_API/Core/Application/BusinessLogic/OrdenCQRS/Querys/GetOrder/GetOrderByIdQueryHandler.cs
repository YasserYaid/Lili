using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetOL;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetOrder
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public GetOrderByIdQueryHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();

                Orden? ordeneRecuperada = await _unidadTrabajo.ordenRepo.ObtenerById(request.IdOrder);


                if (ordeneRecuperada == null)
                {
                    _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_NO_SE_ENCONTRARON_ORDENES;
                    _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_NO_SE_ENCONTRARON_ORDENES;
                }
                else
                {
                    OrdenResponseDTO ordeneResponseDTO = _mapper.Map<OrdenResponseDTO>(ordeneRecuperada);

                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_ORDENES_RECUPERADAS_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_ORDENES_RECUPERADAS_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = ordeneResponseDTO;
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
