using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;

namespace ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration.JoinViewModels
{
    public class OrdenEmpleadoJVMConfiguration : IEntityTypeConfiguration<OrdenEmpleadoJVM>
    {
        public void Configure(EntityTypeBuilder<OrdenEmpleadoJVM> builder)
        {
            builder.ToView("ConsultarOrdenesEmpleadosJVM");
            builder.HasNoKey();
        }
    }
}
