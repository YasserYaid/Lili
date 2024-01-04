using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReposteriaLili_Front.Models.Dominio
{
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }

        public int? IdSucursal { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Nombre { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? ApellidoPaterno { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? ApellidoMaterno { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        public string? Correo { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Telefono { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? UserName { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Cargo { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        public string? Contrasena { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public Sucursal? Sucursal { get; set; }

        public virtual ICollection<Orden>? Ordenes { get; set; }
    }
}
