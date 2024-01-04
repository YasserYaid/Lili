using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios.IVM;
using ReposteriaLili_API.Infraestructure.Internal.Persistence;
using ReposteriaLili_API.Infraestructure.Internal.Repositorio.VM;

namespace ReposteriaLili_API.Infraestructure.Internal.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ReposteriaDBContext _dbContext;
        private readonly string? _secretKey;
        public IClienteRepositorio clienteRepo { get; private set; }
        public IDireccionRepositorio direccionRepo { get; private set; }
        public IEmpleadoRepositorio empleadoRepo { get; private set; }
        public IOrdenRepositorio ordenRepo { get; private set; }
        public IProductoRepositorio productoRepo { get; private set; }
        public ISucursalRepositorio sucursalRepo { get; private set; }
        public ITarjetaRepositorio tarjetaRepo { get; private set; }
        public IViewListarProductosRepositorio viewListarProductosRepo { get; private set; }
        public IViewOrdenesClienteRepositorio viewOrdenesClienteRepo { get; private set; }
        public IViewOrdenesEmpleadoRepositorio viewOrdenesEmpleadoRepo { get; private set; }
        public IViewSesionClienteRepositorio viewSesionClienteRepo { get; private set; }
        public IProductoSucursalRepositorio producto_Sucursal_Repo { get; private set; }
        public IProductoOrdenRepositorio producto_Orden_Repo { get; private set; }

        public UnidadTrabajo(ReposteriaDBContext dBContext, IConfiguration configuration)
        {
            _dbContext = dBContext;
            _secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            empleadoRepo = new EmpleadoRepositorio(dBContext, _secretKey);
            clienteRepo = new ClienteRepositorio(dBContext);
            direccionRepo = new DireccionRepositorio(dBContext);
            ordenRepo = new OrdenRepositorio(dBContext);
            productoRepo = new ProductoRepositorio(dBContext);
            sucursalRepo = new SucursalRepositorio(dBContext);
            tarjetaRepo = new TarjetaRepositorio(dBContext);
            producto_Orden_Repo = new ProductoOrdenRepositorio(dBContext);
            producto_Sucursal_Repo = new ProductoSucursalRepositorio(dBContext);

            viewListarProductosRepo = new ViewListarProductosRepositorio(dBContext);
            viewOrdenesClienteRepo = new ViewOrdenesClienteRepositorio(dBContext);
            viewOrdenesEmpleadoRepo = new ViewOrdenesEmpleadoRepositorio(dBContext);
            viewSesionClienteRepo = new ViewSesionClienteRepositorio(dBContext);
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task Guardar()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
