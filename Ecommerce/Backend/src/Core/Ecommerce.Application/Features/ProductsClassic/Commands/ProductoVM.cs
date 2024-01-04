using Ecommerce.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.ProductsClassic.Commands
{
    public class ProductoVM
    {
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }
        public string? Descripcion { get; set; }
        public int? Rating { get; set; }
        public string? Vendedor { get; set; }
        public int? Stock { get; set; }
        public string? Status { get; set; }
        public int? CategoryId { get; set; }
        public int? CantidadDisponible { get; set; }
        public string? CodigoBarras { get; set; }
        public string? FechaCaducidad { get; set; }
        public string? ImagenCodigoBarrasUrl { get; set; }
        public string? ImagenProductoFirebaseUrl { get; set; }
        public int? BranchId { get; set; }
    }
}
