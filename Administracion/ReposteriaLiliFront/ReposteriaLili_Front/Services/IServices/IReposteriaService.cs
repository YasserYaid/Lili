using ReposteriaLili_Front.Models.DTO;
using ReposteriaLili_Front.Models.DTO.EmpleadosDTOs;
using ReposteriaLili_Front.Models.DTO.OrdenesDTOs;
using ReposteriaLili_Front.Models.DTO.ProductosDTOs;
using ReposteriaLili_Front.Models.DTO.SucursalesDTOs;

namespace ReposteriaLili_Front.Services.IServices
{
    public interface IReposteriaService
    {
        Task<T?> ObtenerOrden<T>(int idOrden);
        Task<T?> ObtenerOrdenClineteInfo<T>(int idOrden);
        Task<T?> EliminarOrden<T>(int idOrden);
        Task<T?> ActualizarOrden<T>(OrdenUpdateDTO ordenUpdateDTO);
        Task<T?> AsingarRepartidorOrden<T>(AssignDeliveryRequestDTO assignDeliveryRequestDTO);        
        Task<T?> RegistrarSucursal<T>(SucursalCreateDTO sucursalCreateDTO);
        Task<T?> RegistrarEmpleado<T>(EmpleadoCreateDTO empleadoCreateDTO);
        Task<T?> RegistrarProducto<T>(ProductoCreateDTO productoCreateDTO);
        Task<T?> ObtenerSucursales<T>();
        Task<T?> ObtenerEmpleados<T>();
        Task<T?> ObtenerPedidosAsignados<T>(int id);
        Task<T?> ObtenerPedidosSolicitadoCliente<T>(int id);
        Task<T?> ObtenerOrdenes<T>();
        Task<T?> ObtenerTodosProductos<T>();
        Task<T?> ObtenerRepartidores<T>();


    }
}
