using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Application.DTO
{
    public class CancelOrderRequestDTO
    {
        public int IdOrden { get; set; }
        public int IdTarjeta { get; set; }
        public int IdCliente { get; set; }
        public int IdDireccion { get; set; }
        public int IdEmpleado { get; set; }
        public int IdSucursal { get; set; }
        public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public decimal? Precio { get; set; }
        public int? CantidadDisponibleTotal { get; set; }
        public string? Categoria { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenUrl { get; set; }
        public string? ImagenCodigoBarrasUrl { get; set; }
        public string? CodigoBarras { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public int CantidadSucursal { get; set; }
        public int CantidadOrdenada { get; set; }
        public decimal PrecioParcial { get; set; }
        public string? NumeroTarjeta { get; set; }
        public string? DescripcionIncidenteCliente { get; set; }
        public string? DescripcionIncidenteRepartidor { get; set; }
        public string? ImagenIncidenteCliente { get; set; }
        public string? ImagenIncidenteRepartidor { get; set; }
        public string? EstadoPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public decimal? ImporteTotalOrden { get; set; }
        public int? NumeroFolio { get; set; }
        public TimeSpan? TiempoEstimadoEntrega { get; set; }
        public TimeSpan? TiempoTardoEntrega { get; set; }
    }
}
