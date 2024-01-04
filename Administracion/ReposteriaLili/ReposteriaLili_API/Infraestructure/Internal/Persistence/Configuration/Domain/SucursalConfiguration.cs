using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration.Domain
{
    public class SucursalConfiguration
    {
        public SucursalConfiguration(EntityTypeBuilder<Sucursal> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(suc => suc.IdSucursal);
            entityTypeBuilder.HasIndex(suc => suc.NombreComercial).IsUnique();
            /*
                        entityTypeBuilder
                            .HasMany(suc => suc.Empleados)
                            .WithOne(emp => emp.Sucursal)
                            .HasForeignKey(emp => emp.IdSucursal)
                            .IsRequired(false)//Para que la llave foranea pueda ser null

                        entityTypeBuilder
                            .HasMany(suc => suc.Ordenes)
                            .WithOne(ord => ord.Sucursal)
                            .HasForeignKey(ord => ord.IdDireccionSucursal)
                            .IsRequired(false);
            */
        }
    }
}
