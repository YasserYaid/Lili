using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration.Domain
{
    public class TarjetaConfiguration
    {
        public TarjetaConfiguration(EntityTypeBuilder<Tarjeta> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(tar => tar.IdTarjeta);
            entityTypeBuilder.Property(tar => tar.IdCliente).IsRequired();
            entityTypeBuilder.HasIndex(tar => tar.NumeroTarjeta).IsUnique();
        }
    }
}
