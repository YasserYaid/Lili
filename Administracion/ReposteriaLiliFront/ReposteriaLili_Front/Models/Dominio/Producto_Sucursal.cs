using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_Front.Models.Dominio
{
    public class Producto_Sucursal
    {
        //       [Key]
        //       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //       public int IdProducto_Sucursal { get; set; }

        public int IdProducto { get; set; }

        public int IdSucursal { get; set; }

        public int Cantidad { get; set; }

        public Producto? Producto { get; set; }

        public Sucursal? Sucursal { get; set; }

    }
}
