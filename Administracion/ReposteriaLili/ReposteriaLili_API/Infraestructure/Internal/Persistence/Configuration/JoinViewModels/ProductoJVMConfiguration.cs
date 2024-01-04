using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;

namespace ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration.JoinViewModels
{
    public class ProductoJVMConfiguration : IEntityTypeConfiguration<ProductoJVM>
    {
        public void Configure(EntityTypeBuilder<ProductoJVM> builder)
        {
            builder.ToView("ConsultarProductosJVM");
            builder.HasNoKey();
        }
    }
}
