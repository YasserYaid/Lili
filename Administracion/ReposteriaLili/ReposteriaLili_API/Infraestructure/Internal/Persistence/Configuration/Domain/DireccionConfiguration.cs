using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration.Domain
{
    public class DireccionConfiguration
    {
        public DireccionConfiguration(EntityTypeBuilder<Direccion> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(dir => dir.IdDireccion);
            //           entityTypeBuilder.Property(dir => dir.IdCliente).IsRequired();
            //           entityTypeBuilder.Property(dir => dir.Estado).IsRequired();
            //           entityTypeBuilder.Property(dir => dir.Ciudad).IsRequired();
            //           entityTypeBuilder.Property(dir => dir.Calle).IsRequired();
            //           entityTypeBuilder.Property(dir => dir.NumeroExterior).IsRequired();
            //           entityTypeBuilder.Property(dir => dir.Colonia).IsRequired();
            //           entityTypeBuilder.Property(dir => dir.CodigoPostal).IsRequired();
            //           entityTypeBuilder.Property(dir => dir.Latitud).IsRequired();
            //           entityTypeBuilder.Property(dir => dir.Longitud).IsRequired();
        }
    }
}
