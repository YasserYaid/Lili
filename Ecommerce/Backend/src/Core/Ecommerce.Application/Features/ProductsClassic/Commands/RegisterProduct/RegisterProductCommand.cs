using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.ProductsClassic.Commands.RegisterProduct
{
    public class RegisterProductCommand : IRequest<ProductoVM>
    {
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }
        public int? Stock { get; set; }
        public int? CategoryId { get; set; }
        public string? Categoria { get; set; }
        public int? CantidadDisponible { get; set; }
        public string? CodigoBarras { get; set; }
        public string? FechaCaducidad { get; set; }
        public string? ImagenCodigoBarrasUrl { get; set; }
        public string? ImagenProductoFirebaseUrl { get; set; }
        public int? BranchId { get; set; }
        public string? Vendedor { get; set; }
        public string? Sucursal {  get; set; }
        public IFormFile? Foto { get; set; }

    }
}
