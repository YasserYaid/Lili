using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_Front.Models.Dominio
{
    public class Tarjeta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTarjeta { get; set; }

        public int IdCliente { get; set; }

        public int? CodigoCVV { get; set; }

        [Column(TypeName = "NVARCHAR(300)")]
        public string? Emisor { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        [Column(TypeName = "NVARCHAR(500)")]
        public string? NombreTitular { get; set; }

        [Column(TypeName = "NVARCHAR(200)")]
        public string? NumeroTarjeta { get; set; }

        [Column(TypeName = "NVARCHAR(30)")]
        public string? TipoTarjeta { get; set; }

        public Cliente? Cliente { get; set; }

        public virtual ICollection<Orden>? Ordenes { get; set; }

    }
}
