using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration.Domain
{
    public class ProductoConfiguration
    {
        public ProductoConfiguration(EntityTypeBuilder<Producto> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(pro => pro.IdProducto);
            entityTypeBuilder.HasIndex(pro => pro.CodigoBarras).IsUnique();
            entityTypeBuilder.HasIndex(pro => pro.ImagenCodigoBarrasUrl).IsUnique();
            entityTypeBuilder.HasIndex(pro => pro.ImagenUrl).IsUnique();
        }
    }
}
