using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;

namespace ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration.JoinViewModels
{
    public class OrdenClienteJVMConfiguration : IEntityTypeConfiguration<OrdenClienteJVM>
    {
        public void Configure(EntityTypeBuilder<OrdenClienteJVM> builder)
        {
            builder.ToView("ConsultarOrdenesClientesJVM");
            builder.HasNoKey();
        }
    }
}
