using System.ComponentModel.DataAnnotations.Schema;

namespace ReposteriaLili_Front.Models.DTO.OrdenesDTOs
{
    public class OrdenResponseDTO
    {
        public int IdOrden { get; set; }
        public int? IdDireccionCliente { get; set; }
        public int IdDireccionSucursal { get; set; }
        public int? IdEmpleadoRepartidor { get; set; }
        public int? IdTarjeta { get; set; }
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
