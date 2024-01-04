using Microsoft.EntityFrameworkCore;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Infraestructure.Internal.Persistence;

namespace ReposteriaLili_API.Infraestructure.Internal.Repositorio
{
    public class OrdenRepositorio : Repositorio<Orden>, IOrdenRepositorio
    {
        private readonly ReposteriaDBContext _dBContext;
        public OrdenRepositorio(ReposteriaDBContext dBContext) : base(dBContext) 
        {
            _dBContext = dBContext;
        }

        public async Task<bool> ActualizarOrden(Orden ordenEntrante)
        {
            _dBContext.Ordenes.Attach(ordenEntrante);
            _dBContext.Entry(ordenEntrante).State = EntityState.Modified;
            int filasAfectadas = await _dBContext.SaveChangesAsync();
            if (filasAfectadas > 0) return true;
            else return false;
        }

        public async Task<Orden?> AsignarRepartidorOrden(int idOrden, int idRepartidor)
        {
            Orden? ordenAEditar = _dBContext.Ordenes.FirstOrDefault(ord => ord.IdOrden == idOrden);
            if (ordenAEditar != null)
            {
                ordenAEditar.IdEmpleadoRepartidor = idRepartidor;
                ordenAEditar.EstadoPedido = "En proceso de entrega";
            }
            await _dBContext.SaveChangesAsync();
            return ordenAEditar;
        }

    }
}
