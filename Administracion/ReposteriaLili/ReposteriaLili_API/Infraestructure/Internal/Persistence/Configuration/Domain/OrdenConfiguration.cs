using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration.Domain
{
    public class OrdenConfiguration
    {
        public OrdenConfiguration(EntityTypeBuilder<Orden> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(ord => ord.IdOrden);
            entityTypeBuilder.Property(ord => ord.IdDireccionCliente).IsRequired();
            entityTypeBuilder.Property(ord => ord.IdDireccionSucursal).IsRequired();
            entityTypeBuilder.Property(ord => ord.IdTarjeta).IsRequired();
            entityTypeBuilder.HasIndex(ord => ord.NumeroFolio).IsUnique();
            entityTypeBuilder.HasIndex(ord => ord.ImagenIncidenteCliente).IsUnique();
            entityTypeBuilder.HasIndex(ord => ord.ImagenIncidenteRepartidor).IsUnique();
        }
    }
}
