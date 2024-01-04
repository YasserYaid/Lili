using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_Front.Models.Dominio
{
    public class Direccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDireccion { get; set; }

        public int IdCliente { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Estado { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Ciudad { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Municipio { get; set; }

        [Column(TypeName = "NVARCHAR(200)")]
        public string? Calle { get; set; }

        public int? NumeroExterior { get; set; }

        public int? NumeroInterior { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        public string? Colonia { get; set; }

        [Column(TypeName = "NVARCHAR(20)")]
        public string? CodigoPostal { get; set; }

        [Column(TypeName = "NVARCHAR(200)")]
        public string? Latitud { get; set; }

        [Column(TypeName = "NVARCHAR(200)")]
        public string? Longitud { get; set; }

        public Cliente? Cliente { get; set; }

        public virtual ICollection<Orden>? Ordenes { get; set; }

    }
}
