using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Dominio
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Nombre { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal? Precio { get; set; }

        public int? CantidadDisponible { get; set; }

        [Column(TypeName = "NVARCHAR(300)")]
        public string? Categoria { get; set; }

        [Column(TypeName = "NVARCHAR(3000)")]
        public string? Descripcion { get; set; }

        [Column(TypeName = "NVARCHAR(3000)")]
        public string? ImagenUrl { get; set; }

        [Column(TypeName = "NVARCHAR(3000)")]
        public string? ImagenCodigoBarrasUrl { get; set; }

        [Column(TypeName = "NVARCHAR(300)")]
        public string? CodigoBarras { get; set; }

        public DateTime? FechaCaducidad { get; set; }

        public virtual ICollection<Sucursal>? Sucursales { get; set; }

        public virtual ICollection<Producto_Sucursal>? ProductoSucursal { get; set; }

        public virtual ICollection<Orden>? Ordenes { get; set; }

        public virtual ICollection<Producto_Orden>? ProductoOrden { get; set; }

    }
}
