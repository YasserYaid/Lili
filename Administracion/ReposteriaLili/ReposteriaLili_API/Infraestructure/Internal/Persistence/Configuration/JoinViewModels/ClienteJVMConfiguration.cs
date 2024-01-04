using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;

namespace ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration.JoinViewModels
{
    public class ClienteJVMConfiguration : IEntityTypeConfiguration<ClienteJVM>
    {
        public void Configure(EntityTypeBuilder<ClienteJVM> builder)
        {
            builder.ToView("IniciarSesionClienteJVM");
            builder.HasNoKey();
        }
    }
}
