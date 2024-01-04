using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.ProductosDTOs;
using ReposteriaLili_API.Core.Application.DTO.SucursalesDTOs;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;
using ReposteriaLili_API.Infraestructure.Internal.Repositorio;
using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.SucursalesCQRS.Querys.GetBList
{
    public class GetBranchListQueryHandler : IRequestHandler<GetBranchListQuery, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public GetBranchListQueryHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(GetBranchListQuery request, CancellationToken cancellationToken)
        {
            try
            {                
                _dataBaseResponse.LimpiarValores();

                List<Sucursal> sucursalesRecuperadas = await _unidadTrabajo.sucursalRepo.ObtenerTodos();


                if (sucursalesRecuperadas.IsNullOrEmpty())
                {
                    _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_NO_SE_ENCONTRARON_SUCURSALES;
                    _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_NO_SE_ENCONTRARON_SUCURSALES;
                }
                else
                {
                    IEnumerable<SucursalResponseDTO> sucursalesResponseListDTO = _mapper.Map<IEnumerable<SucursalResponseDTO>>(sucursalesRecuperadas);

                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU07_CODIGO_SUCURSALES_RECUPERADAS_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU07_MENSAJE_SUCURSALES_RECUPERADAS_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = sucursalesResponseListDTO;
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
