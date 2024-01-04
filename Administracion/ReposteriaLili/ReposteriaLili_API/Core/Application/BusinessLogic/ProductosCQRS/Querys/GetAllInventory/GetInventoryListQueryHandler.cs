using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Querys.GetEmployeeList;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.Internal.Repositorio;
using ReposteriaLili_API.Core.Application.DTO.ProductosDTOs;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Querys.GetAllInventory
{
    public class GetInventoryListQueryHandler : IRequestHandler<GetInventoryListQuery, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public GetInventoryListQueryHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(GetInventoryListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();

                List<Producto> productosRecuperados = await _unidadTrabajo.productoRepo.ObtenerTodos();


                if (productosRecuperados.Count() <= 0)
                {
                    _dataBaseResponse.ControlCode = Constantes.CU12_CODIGO_NO_ENCONTRO_PRODUCTO_EN_INVENTARIO;
                    _dataBaseResponse.ControlMessage = Constantes.CU12_MENSAJE_NO_ENCONTRO_PRODUCTO_EN_INVENTARIO;
                }
                else
                {
                    IEnumerable<ProductoResponseDTO> productosResponseListDTO = _mapper.Map<IEnumerable<ProductoResponseDTO>>(productosRecuperados);

                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU12_CODIGO_INVENTARIO_RECUPERADO_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU12_MENSAJE_INVENTARIO_RECUPERADO_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = productosResponseListDTO;
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
