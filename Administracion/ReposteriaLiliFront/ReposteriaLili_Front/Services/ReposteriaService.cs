using ReposteriaLili_Front.Models.DTO;
using ReposteriaLili_Front.Models.DTO.EmpleadosDTOs;
using ReposteriaLili_Front.Models.DTO.OrdenesDTOs;
using ReposteriaLili_Front.Models.DTO.ProductosDTOs;
using ReposteriaLili_Front.Models.DTO.SucursalesDTOs;
using ReposteriaLili_Front.Models.ReqResModels;
using ReposteriaLili_Front.Services.IServices;
using static ReposteriaLili_Front.Models.Constantes.OpcionesAPI;

namespace ReposteriaLili_Front.Services
{
    public class ReposteriaService : BaseService, IReposteriaService
    {
        public readonly IHttpClientFactory _httpClient;
        public string? _reposteriaLiliAPIUrl;

        public ReposteriaService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            _reposteriaLiliAPIUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T?> RegistrarSucursal<T>(SucursalCreateDTO sucursalCreateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.POST,
                Data = sucursalCreateDTO,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Sucursal/Registrar"
            });
        }

        public Task<T?> ActualizarOrden<T>(OrdenUpdateDTO ordenUpdateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.PUT,
                Data = ordenUpdateDTO,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Orden/Actualizar/" + ordenUpdateDTO.IdOrden
            });
        }

        public Task<T?> AsingarRepartidorOrden<T>(AssignDeliveryRequestDTO assignDeliveryRequestDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.PUT,
                Data = assignDeliveryRequestDTO,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Orden/AsignarRepartidor/" + assignDeliveryRequestDTO.IdOrden
            });
        }

        public Task<T?> ObtenerSucursales<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.GET,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Sucursal/ListarSucursales"
            });
        }

        public Task<T?> EliminarOrden<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.DELETE,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Orden/Eliminar/" + id
            });
        }

        public Task<T?> ObtenerEmpleados<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.GET,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Empleado/ListarEmpleados"
            });
        }

        public Task<T?> RegistrarEmpleado<T>(EmpleadoCreateDTO empleadoCreateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.POST,
                Data = empleadoCreateDTO,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Empleado/Registrar"
            });
        }

        public Task<T?> RegistrarProducto<T>(ProductoCreateDTO productoCreateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.POST,
                Data = productoCreateDTO,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Producto/Registrar"
            });
        }

        public Task<T?> ObtenerPedidosAsignados<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.GET,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Empleado/PedidosAsignados?IdEmpleado=" + id
            });
        }
        

        public Task<T?> ObtenerPedidosSolicitadoCliente<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.GET,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Cliente/Pedidos?IdCliente=" + id
            });
        }

        public Task<T?> ObtenerOrdenes<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.GET,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Orden/ListarOrdenes"
            });
        }

        public Task<T?> ObtenerTodosProductos<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.GET,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Producto/ListarInventario"
            });        
        }

        public Task<T?> ObtenerRepartidores<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.GET,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Empleado/ListarRepartidores"
            });
        }

        public Task<T?> ObtenerOrden<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.GET,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Orden/Detalle/" + id
            });
        }

        public Task<T?> ObtenerOrdenClineteInfo<T>(int idOrden)
        {
            return SendAsync<T>(new APIRequest()
            {
                OperacionHTTP = OperacionHTTP.GET,
                ApiUrlDestino = _reposteriaLiliAPIUrl + "Cliente/PedidoInfo/" + idOrden
            });
        }
    }
}
