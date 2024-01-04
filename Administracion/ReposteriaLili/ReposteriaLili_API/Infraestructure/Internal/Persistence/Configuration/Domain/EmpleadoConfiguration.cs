using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration.Domain
{
    public class EmpleadoConfiguration
    {
        public EmpleadoConfiguration(EntityTypeBuilder<Empleado> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(emp => emp.IdEmpleado);
            //            entityTypeBuilder.Property(emp => emp.IdSucursal).IsRequired();
            //            entityTypeBuilder.Property(emp => emp.Nombre).IsRequired();
            //            entityTypeBuilder.Property(emp => emp.ApellidoPaterno).IsRequired();
            //            entityTypeBuilder.Property(emp => emp.ApellidoMaterno).IsRequired();
            //            entityTypeBuilder.Property(emp => emp.Correo).IsRequired();
            //            entityTypeBuilder.Property(emp => emp.Telefono).IsRequired();
            //            entityTypeBuilder.Property(emp => emp.UserName).IsRequired();
            //            entityTypeBuilder.Property(emp => emp.Cargo).IsRequired();
            //            entityTypeBuilder.Property(emp => emp.Contrasena).IsRequired();
            //            entityTypeBuilder.Property(emp => emp.FechaIngreso).IsRequired();
            entityTypeBuilder.HasIndex(emp => emp.Correo).IsUnique();
            entityTypeBuilder.HasIndex(emp => emp.Telefono).IsUnique();
            entityTypeBuilder.HasIndex(emp => emp.UserName).IsUnique();
        }
    }
}
