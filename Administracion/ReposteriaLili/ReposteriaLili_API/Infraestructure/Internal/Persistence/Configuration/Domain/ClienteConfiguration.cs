using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration.Domain
{
    public class ClienteConfiguration
    {
        public ClienteConfiguration(EntityTypeBuilder<Cliente> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(cli => cli.IdCliente);
            //            entityTypeBuilder.Property(cli => cli.Nombre).IsRequired();
            //            entityTypeBuilder.Property(cli => cli.ApellidoPaterno).IsRequired();
            //            entityTypeBuilder.Property(cli => cli.ApellidoMaterno).IsRequired();
            //            entityTypeBuilder.Property(cli => cli.Correo).IsRequired();
            //            entityTypeBuilder.Property(cli => cli.Telefono).IsRequired();
            //            entityTypeBuilder.Property(cli => cli.UserName).IsRequired();
            //            entityTypeBuilder.Property(cli => cli.Contrasena).IsRequired();
            //            entityTypeBuilder.Property(cli => cli.FechaNacimiento).IsRequired();
            entityTypeBuilder.HasIndex(cli => cli.Correo).IsUnique();
            entityTypeBuilder.HasIndex(cli => cli.Telefono).IsUnique();
            entityTypeBuilder.HasIndex(cli => cli.UserName).IsUnique();
            /*
                        entityTypeBuilder.HasOne(cli => cli.Tarjeta)//Se exparse la llave primaria de esta entidad
                            .WithOne(tar => tar.Cliente)
                            .HasForeignKey<Tarjeta>(tar => tar.IdCliente)//Con esto se especifica que esta sera la llave foranea que se exparcio
                            .IsRequired(true);// Creo que significa que se necesita el id cliente para registrar tarjeta

                        entityTypeBuilder
                            .HasOne(cli => cli.Direccion)
                            .WithOne(dir => dir.Cliente)
                            .HasForeignKey<Direccion>(dir => dir.IdCliente)
                            .IsRequired(true);
            */
        }
    }
}
