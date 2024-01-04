using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Dominio
{
    public class Orden
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrden { get; set; }

        public int? IdDireccionCliente { get; set; }

        public int IdDireccionSucursal { get; set; }

        public int? IdEmpleadoRepartidor { get; set; }

        public int? IdTarjeta { get; set; }

        [Column(TypeName = "NVARCHAR(3000)")]
        public string? DescripcionIncidenteCliente { get; set; }

        [Column(TypeName = "NVARCHAR(3000)")]
        public string? DescripcionIncidenteRepartidor { get; set; }

        [Column(TypeName = "NVARCHAR(3000)")]
        public string? ImagenIncidenteCliente { get; set; }

        [Column(TypeName = "NVARCHAR(3000)")]
        public string? ImagenIncidenteRepartidor { get; set; }

        [Column(TypeName = "NVARCHAR(30)")]
        public string? EstadoPedido { get; set; }

        public DateTime? FechaEntrega { get; set; }

        public DateTime? FechaSolicitud { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal? ImporteTotalOrden { get; set; }

        public int? NumeroFolio { get; set; }

        public TimeSpan? TiempoEstimadoEntrega { get; set; }

        public TimeSpan? TiempoTardoEntrega { get; set; }

        public Sucursal? Sucursal { get; set; }

        public Empleado? Empleado { get; set; }

        public Tarjeta? Tarjeta { get; set; }

        public Direccion? Direccion { get; set; }

        public virtual ICollection<Producto>? Productos { get; set; }

        public virtual ICollection<Producto_Orden>? ProductoOrden { get; set; }

    }
}
