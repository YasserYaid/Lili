using AutoMapper;
using MediatR;
using ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Commands.CreateOrder;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.Internal.Repositorio;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public DeleteOrderCommandHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();
                
                Orden? ordenAEliminar = await _unidadTrabajo.ordenRepo.Obtener(ord => ord.IdOrden == request.idOrden);
                
                bool errorActualizandoCantidad = false;

                if (ordenAEliminar != null)
                {
                    var productos_orden = _unidadTrabajo.producto_Orden_Repo.ObtenerTodos().Result.Where(proord => proord.IdOrden == request.idOrden);

                    if(productos_orden.Count() > 0)
                    {
                        foreach (var productoOrdenado in productos_orden)
                        {
                            var productoEncontrado = await _unidadTrabajo.productoRepo.Obtener(pro => pro.IdProducto == productoOrdenado.IdProducto);
                            var productoEncontradoSucursal = await _unidadTrabajo.producto_Sucursal_Repo.Obtener(pro => pro.IdProducto == productoOrdenado.IdProducto);

                            if (productoEncontrado != null)
                            {
                                productoEncontrado.CantidadDisponible = productoEncontrado.CantidadDisponible + productoOrdenado.Cantidad;
                                await _unidadTrabajo.productoRepo.ActualizarCantidad(productoEncontrado.IdProducto, (int)productoEncontrado.CantidadDisponible);
                            }
                            else
                            {
                                _dataBaseResponse.ControlCode = Constantes.CU06_CODIGO_NO_SE_RECUPERO_INFO_TABLA_PRODUCTO;
                                _dataBaseResponse.ControlMessage = Constantes.CU06_MENSAJE_NO_SE_RECUPERO_INFO_TABLA_PRODUCTO;
                                errorActualizandoCantidad = true;
                                break;
                            }
                            if (productoEncontradoSucursal != null)
                            {
                                productoEncontradoSucursal.Cantidad = productoEncontradoSucursal.Cantidad + productoOrdenado.Cantidad;
                                await _unidadTrabajo.producto_Sucursal_Repo.ActualizarCantidad(productoEncontradoSucursal.IdProducto, productoEncontradoSucursal.IdSucursal, productoEncontradoSucursal.Cantidad);
                            }
                            else
                            {
                                _dataBaseResponse.ControlCode = Constantes.CU06_CODIGO_NO_SE_RECUPERO_INFO_TABLA_PRODUCTO_SUCURSAL;
                                _dataBaseResponse.ControlMessage = Constantes.CU06_MENSAJE_NO_SE_RECUPERO_INFO_TABLA_PRODUCTO_SUCURSAL;
                                errorActualizandoCantidad = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        _dataBaseResponse.ControlCode = Constantes.CU06_CODIGO_NO_SE_RECUPERO_INFO_TABLA_PRODUCTO_ORDEN;
                        _dataBaseResponse.ControlMessage = Constantes.CU06_MENSAJE_NO_SE_RECUPERO_INFO_TABLA_PRODUCTO_ORDEN;
                        errorActualizandoCantidad = true;
                    }
 
                    if (!errorActualizandoCantidad)
                    {
                        await _unidadTrabajo.ordenRepo.Eliminar(ordenAEliminar);
                        _dataBaseResponse.isExitoso = true;
                        _dataBaseResponse.ControlCode = Constantes.CU06_CODIGO_ORDEN_CANCELADA_SATISFATORIAMENTE;
                        _dataBaseResponse.ControlMessage = Constantes.CU06_MENSAJE_ORDEN_CANCELADA_SATISFATORIAMENTE;
                    }

                }
                else
                {
                    _dataBaseResponse.ControlCode = Constantes.CU06_CODIGO_NO_SE_ENCONTRO_ORDEN;
                    _dataBaseResponse.ControlMessage = Constantes.CU06_MENSAJE__NO_SE_ENCONTRO_ORDEN;
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
