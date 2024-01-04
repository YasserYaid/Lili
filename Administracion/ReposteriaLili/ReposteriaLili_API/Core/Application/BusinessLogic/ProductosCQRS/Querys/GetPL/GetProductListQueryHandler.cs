using AutoMapper;
using MediatR;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;
using Microsoft.IdentityModel.Tokens;
using ReposteriaLili_API.Core.Application.DTO.ProductosDTOs;
using ReposteriaLili_API.Core.Application.DTO.SucursalesDTOs;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Querys.GetPL
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;
        private CatalogoProductosResponse catalogoProductosResponseDTO;
        private IEnumerable<ProductoResponseDTO> foundListProductosResponseDTO;
        private SucursalResponseDTO foundSucursalResponseDTO;

        public GetProductListQueryHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<ProductoJVM> productosFoundInfo = null;

                _dataBaseResponse.LimpiarValores();

                if ( (request.nombre.IsNullOrEmpty() || request.nombre.ToUpper().Equals("TODOS")) && (request.categoria.IsNullOrEmpty() || request.categoria.ToUpper().Equals("TODOS")))
                {
                    productosFoundInfo = _unidadTrabajo.viewListarProductosRepo.ObtenerTodos().Result.Where(projvm => projvm.IdSucursal == request.idSucursal);
                }
                else if(request.nombre.IsNullOrEmpty() || request.nombre.ToUpper().Equals("TODOS"))
                {
                    productosFoundInfo = _unidadTrabajo.viewListarProductosRepo.ObtenerTodos().Result.Where(projvm => projvm.IdSucursal == request.idSucursal).Where(projvm => projvm.Categoria.ToLower() == request.categoria.ToLower());
                }
                else if (request.categoria.IsNullOrEmpty() || request.categoria.ToUpper().Equals("TODOS"))
                {
                    productosFoundInfo = _unidadTrabajo.viewListarProductosRepo.ObtenerTodos().Result.Where(projvm => projvm.IdSucursal == request.idSucursal).Where(projvm => projvm.Nombre.ToLower() == request.nombre.ToLower());
                }
                else
                {
                    productosFoundInfo = _unidadTrabajo.viewListarProductosRepo.ObtenerTodos().Result.Where(projvm => projvm.IdSucursal == request.idSucursal).Where(projvm => projvm.Categoria.ToLower() == request.categoria.ToLower()).Where(projvm => projvm.Nombre.ToLower() == request.nombre.ToLower());
                }

                if (productosFoundInfo.IsNullOrEmpty())
                {
                    _dataBaseResponse.ControlCode = Constantes.CU03_CODIGO_NO_SE_ENCONTRARON_PRODUCTOS;
                    _dataBaseResponse.ControlMessage = Constantes.CU03_MENSAJE_NO_SE_ENCONTRARON_PRODUCTOS;
                }
                else
                {
                    foundListProductosResponseDTO = _mapper.Map<IEnumerable<ProductoResponseDTO>>(productosFoundInfo);
                    foundSucursalResponseDTO = _mapper.Map<SucursalResponseDTO>(productosFoundInfo.First());

                    catalogoProductosResponseDTO = new CatalogoProductosResponse();
                    catalogoProductosResponseDTO.ProductoResponseDtoList = foundListProductosResponseDTO;
                    catalogoProductosResponseDTO.SucursalResponseDto = foundSucursalResponseDTO;

                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU03_CODIGO_PRODUCTOS_RECUPERADOS_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU03_MENSAJE_PRODUCTOS_RECUPERADOS_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = catalogoProductosResponseDTO;
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
