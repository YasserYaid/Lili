using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios.IVM;

namespace ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios
{
    public interface IUnidadTrabajo : IDisposable
    {
        IProductoSucursalRepositorio producto_Sucursal_Repo { get; }
        IProductoOrdenRepositorio producto_Orden_Repo { get; }
        IClienteRepositorio clienteRepo { get; }
        IDireccionRepositorio direccionRepo { get; }
        IEmpleadoRepositorio empleadoRepo { get; }
        IOrdenRepositorio ordenRepo { get; }
        IProductoRepositorio productoRepo { get; }
        ISucursalRepositorio sucursalRepo { get; }
        ITarjetaRepositorio tarjetaRepo { get; }
        IViewListarProductosRepositorio viewListarProductosRepo { get; }
        IViewOrdenesClienteRepositorio viewOrdenesClienteRepo { get; }
        IViewOrdenesEmpleadoRepositorio viewOrdenesEmpleadoRepo { get; }
        IViewSesionClienteRepositorio viewSesionClienteRepo { get; }

        Task Guardar();
//        int Complete();

    }
}
