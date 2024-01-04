using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Dominio
{
    public class Producto_Orden
    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int IdProducto_Orden { get; set; }

        public int IdProducto { get; set; }

        public int IdOrden { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal PrecioParcial { get; set; }

        public Producto? Producto { get; set; }

        public Orden? Orden { get; set; }
    }
}
