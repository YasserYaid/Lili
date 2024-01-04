using AutoMapper;
using MediatR;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio;
using System.Security.Cryptography;
using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public CreateOrderCommandHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();
                Orden ordenARegistrar = new Orden();
                ordenARegistrar.IdDireccionCliente = request.cartOrderCreateDTO.ordenCreateRequestDTO.IdDireccionCliente;
                ordenARegistrar.IdTarjeta = request.cartOrderCreateDTO.ordenCreateRequestDTO.IdTarjeta;
                ordenARegistrar.IdDireccionSucursal = request.cartOrderCreateDTO.ordenCreateRequestDTO.IdSucursal;
                ordenARegistrar.EstadoPedido = Constantes.ESTADO_ORDEN_PROCESO_DE_VALIDACION;
                ordenARegistrar.TiempoEstimadoEntrega = DateTime.Now.TimeOfDay;
                ordenARegistrar.FechaSolicitud = DateTime.Now;
                
                bool isFolioDuplicado = true;
                while (isFolioDuplicado)
                {
                    ordenARegistrar.NumeroFolio = GenerarNumeroFolio();

                    if(ordenARegistrar.NumeroFolio < 0)
                    {
                        ordenARegistrar.NumeroFolio = ordenARegistrar.NumeroFolio * -1;
                    }

                    isFolioDuplicado = await ComprobarFolio(ordenARegistrar.NumeroFolio);
                }
                decimal importe = 0;

                foreach(var product in request.cartOrderCreateDTO.productosOrderRequestDTOs)
                {
                    decimal importeParcial = (decimal)(product.Precio * product.CantidadOrdenada);
                    importe = importe + importeParcial;
                }

                ordenARegistrar.ImporteTotalOrden = importe;
                ordenARegistrar = await _unidadTrabajo.ordenRepo.RegistrarRetornandoConID(ordenARegistrar);

                if(ordenARegistrar.IdOrden > 0)
                {
                    Producto_Orden producto_Orden_db_table = new Producto_Orden();
                    producto_Orden_db_table.IdOrden = ordenARegistrar.IdOrden;

                    foreach (var producto in request.cartOrderCreateDTO.productosOrderRequestDTOs)
                    {
                        producto_Orden_db_table.IdProducto = producto.IdProducto;
                        producto_Orden_db_table.Cantidad = producto.CantidadOrdenada;
                        producto_Orden_db_table.PrecioParcial = (decimal)(producto.Precio * producto.CantidadOrdenada);

                        producto.CantidadSucursal = producto.CantidadSucursal - producto.CantidadOrdenada;
                        producto.CantidadDisponible = producto.CantidadDisponible - producto.CantidadOrdenada;

                        await _unidadTrabajo.producto_Orden_Repo.Registrar(producto_Orden_db_table);
                        await _unidadTrabajo.producto_Sucursal_Repo.ActualizarCantidad(producto.IdProducto, producto.IdSucursal, producto.CantidadSucursal);
                        await _unidadTrabajo.productoRepo.ActualizarCantidad(producto.IdProducto, (int) producto.CantidadDisponible);
                    }

                    OrdenResponseDTO ordenResponseDTO = _mapper.Map<OrdenResponseDTO>(ordenARegistrar);

                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU04_CODIGO_REGISTRO_ORDEN_SATISFACTORIO;
                    _dataBaseResponse.ControlMessage = Constantes.CU04_MENSAJE_REGISTRO_ORDEN_SATISFACTORIO;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = ordenResponseDTO;
                }
                else
                {
                    _dataBaseResponse.ControlCode = Constantes.CU04_CODIGO_REGISTRO_ORDEN_FALLIDO;
                    _dataBaseResponse.ControlMessage = Constantes.CU04_MENSAJE_REGISTRO_ORDEN_FALLIDO;
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

        private async Task<bool> ComprobarFolio (int? numeroFolio)
        {
            bool isFolioDuplicado = false;
            if (await _unidadTrabajo.ordenRepo.Obtener(ord => ord.NumeroFolio == numeroFolio) != null) isFolioDuplicado = true;
            return isFolioDuplicado;
        }

        private static int GenerarNumeroFolio()
        {
            using (RNGCryptoServiceProvider rngCrypt = new RNGCryptoServiceProvider())
            {
                byte[] tokenBuffer = new byte[4];        // `int32` toma 4 bytes en C#
                rngCrypt.GetBytes(tokenBuffer);
                return BitConverter.ToInt32(tokenBuffer, 0);
            }
        }
    }
}
