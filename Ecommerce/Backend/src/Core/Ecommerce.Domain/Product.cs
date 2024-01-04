using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Domain;

public class Product : BaseDomainModel {
    
    [Column(TypeName = "NVARCHAR(100)")]
    public string? Nombre { get; set; }

    [Column(TypeName = "DECIMAL(10,2)")]
    public decimal Precio { get; set; }

    [Column(TypeName = "NVARCHAR(4000)")]
    public string? Descripcion { get; set; }

    public int Rating { get; set; }

    [Column(TypeName = "NVARCHAR(100)")]
    public string? Vendedor { get; set; }

    public int Stock {get;set;}

    public ProductStatus Status { get; set; } = ProductStatus.Activo;

    public int CategoryId { get; set; }
    public Category? Category {get;set;}

    public virtual ICollection<Review>? Reviews { get; set; }

    public virtual ICollection<Image>? Images { get; set; }
    [Column("cantidadDisponible")]
    public int? CantidadDisponible { get; set; }
    [Column("codigoBarras")]
    public string? CodigoBarras { get; set;}
    [Column("fechaCaducidad")]
    public string? FechaCaducidad { get; set; }
    [Column("imagenCodigoBarrasUrl" , TypeName = "NVARCHAR(4000)")]
    public string? ImagenCodigoBarrasUrl { get; set; }
    [Column("imagenProductoFirebaseUrl", TypeName = "NVARCHAR(4000)")]
    public string? ImagenProductoFirebaseUrl { get; set; }
    public Branch? Branch { get; set; }

    [Column("BranchId")]
    public int? BranchId { get; set; }

}