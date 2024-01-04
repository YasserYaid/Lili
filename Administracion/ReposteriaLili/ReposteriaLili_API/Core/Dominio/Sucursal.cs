using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Dominio
{
    public class Sucursal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSucursal { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Estado { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Ciudad { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Municipio { get; set; }

        [Column(TypeName = "NVARCHAR(200)")]
        public string? Calle { get; set; }

        public int? Numero { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        public string? Colonia { get; set; }

        [Column(TypeName = "NVARCHAR(20)")]
        public string? CodigoPostal { get; set; }

        [Column(TypeName = "NVARCHAR(200)")]
        public string? Latitud { get; set; }

        [Column(TypeName = "NVARCHAR(200)")]
        public string? Longitud { get; set; }

        [Column(TypeName = "NVARCHAR(20)")]
        public string? DiaInicial { get; set; }

        [Column(TypeName = "NVARCHAR(20)")]
        public string? DiaFinal { get; set; }

        public TimeSpan? HoraInicial { get; set; }

        public TimeSpan? HoraFinal { get; set; }

        [Column(TypeName = "NVARCHAR(300)")]
        public string? NombreComercial { get; set; }

        public virtual ICollection<Empleado>? Empleados { get; set; }

        public virtual ICollection<Orden>? Ordenes { get; set; }

        public virtual ICollection<Producto>? Productos { get; set; }

        public virtual ICollection<Producto_Sucursal>? ProductoSucursal {  get; set; }
    }
}
