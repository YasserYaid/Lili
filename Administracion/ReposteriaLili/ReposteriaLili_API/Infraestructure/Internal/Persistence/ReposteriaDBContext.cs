using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;
using ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration;
using ReposteriaLili_API.Infraestructure.Internal.Persistence.Configuration.JoinViewModels;

namespace ReposteriaLili_API.Infraestructure.Internal.Persistence
{
    public class ReposteriaDBContext : DbContext
    {
        public ReposteriaDBContext(DbContextOptions<ReposteriaDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sucursal>()
                .HasMany(suc => suc.Empleados)
                .WithOne(emp => emp.Sucursal)
                .HasForeignKey(emp => emp.IdSucursal)
                .IsRequired(false)//Para que la llave foranea pueda ser null
                .OnDelete(DeleteBehavior.Restrict);//Creo que significa si se elimina una sucursa el Empleado no se elimina

            modelBuilder.Entity<Sucursal>()
                .HasMany(suc => suc.Ordenes)
                .WithOne(ord => ord.Sucursal)
                .HasForeignKey(ord => ord.IdDireccionSucursal)
                .IsRequired(false);

            modelBuilder.Entity<Empleado>()
                .HasMany(emp => emp.Ordenes)
                .WithOne(ord => ord.Empleado)
                .HasForeignKey(ord => ord.IdEmpleadoRepartidor)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tarjeta>()
                .HasMany(tar => tar.Ordenes)
                .WithOne(ord => ord.Tarjeta)
                .HasForeignKey(ord => ord.IdTarjeta)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Direccion>()
                .HasMany(dir => dir.Ordenes)
                .WithOne(ord => ord.Direccion)
                .HasForeignKey(ord => ord.IdDireccionCliente)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sucursal>()
                .HasMany(suc => suc.Productos)
                .WithMany(pro => pro.Sucursales)
                .UsingEntity<Producto_Sucursal>(
                    prosuc => prosuc.HasOne(prosuc => prosuc.Producto).WithMany(pro => pro.ProductoSucursal).HasForeignKey(prosuc => prosuc.IdProducto),
                    prosuc => prosuc.HasOne(prosuc => prosuc.Sucursal).WithMany(suc => suc.ProductoSucursal).HasForeignKey(prosuc => prosuc.IdSucursal)
                );

            modelBuilder.Entity<Producto>()
                .HasMany(pro => pro.Ordenes)
                .WithMany(ord => ord.Productos)
                .UsingEntity<Producto_Orden>(
                    proord => proord.HasOne(proord => proord.Orden).WithMany(ord => ord.ProductoOrden).HasForeignKey(proord => proord.IdOrden),
                    proord => proord.HasOne(proord => proord.Producto).WithMany(pro => pro.ProductoOrden).HasForeignKey(proord => proord.IdProducto)
                );

            modelBuilder.Entity<Cliente>()//Se exparse la llave primaria de esta entidad
                .HasOne(cli => cli.Tarjeta)
                .WithOne(tar => tar.Cliente)
                .HasForeignKey<Tarjeta>(tar => tar.IdCliente)//Con esto se especifica que esta sera la llave foranea que se exparsion
                .IsRequired(true);// Creo que significa que se necesita el id cliente para registrar tarjeta

            modelBuilder.Entity<Cliente>()
                .HasOne(cli => cli.Direccion)
                .WithOne(dir => dir.Cliente)
                .HasForeignKey<Direccion>(dir => dir.IdCliente)
            .IsRequired(true);

            /////ESTE BLOQUE SE PUEDE DESACTIVAR PARA COMODIDAD Y ACTIVARLO YA PARA EL FINAL LO IMPORTANTE ES LO DE ARRIBA YA QUE SON LAS RELACIONES DE LAS TABLAS, ESTAS SON RESTRICIONES DE NULLABLE Y UNIQUE PERO INDEXADO EL OTRO QUIEN SABE
            modelBuilder.Entity<Cliente>().HasIndex(cli => cli.Correo).IsUnique();
            modelBuilder.Entity<Cliente>().HasIndex(cli => cli.Telefono).IsUnique();
            modelBuilder.Entity<Cliente>().HasIndex(cli => cli.UserName).IsUnique();

            modelBuilder.Entity<Empleado>().HasIndex(emp => emp.Correo).IsUnique();
            modelBuilder.Entity<Empleado>().HasIndex(emp => emp.Telefono).IsUnique();
            modelBuilder.Entity<Empleado>().HasIndex(emp => emp.UserName).IsUnique();

            modelBuilder.Entity<Orden>().Property(ord => ord.IdDireccionCliente).IsRequired();
            modelBuilder.Entity<Orden>().Property(ord => ord.IdTarjeta).IsRequired();
            modelBuilder.Entity<Orden>().Property(ord => ord.IdDireccionSucursal).IsRequired();
            modelBuilder.Entity<Orden>().HasIndex(ord => ord.NumeroFolio).IsUnique();
            modelBuilder.Entity<Orden>().HasIndex(ord => ord.ImagenIncidenteCliente).IsUnique();
            modelBuilder.Entity<Orden>().HasIndex(ord => ord.ImagenIncidenteRepartidor).IsUnique();

            modelBuilder.Entity<Producto>().HasIndex(pro => pro.CodigoBarras).IsUnique();
            modelBuilder.Entity<Producto>().HasIndex(pro => pro.ImagenCodigoBarrasUrl).IsUnique();
            modelBuilder.Entity<Producto>().HasIndex(pro => pro.ImagenUrl).IsUnique();

            modelBuilder.Entity<Sucursal>().HasIndex(suc => suc.NombreComercial).IsUnique();

            modelBuilder.Entity<Tarjeta>().Property(tar => tar.IdCliente).IsRequired();
            modelBuilder.Entity<Tarjeta>().HasIndex(tar => tar.NumeroTarjeta).IsUnique();

            modelBuilder.Entity<Direccion>().Property(dir => dir.IdCliente).IsRequired();

            //////////

            /////////// JOINVIEWMODELS BLOQUE PARA CREAR LAS VISTAS EN LA BASE DE DATOS ELMINAR ESTE BLOQUE DAÑA CASOS DE USO ESTE BLOQUE DEPENDE DE LA MIGRACION CON EL MISMO NOMBRE SI SE ELIMINA LA MIGRACION ESTO NO SIRVE
            modelBuilder.ApplyConfiguration(new ClienteJVMConfiguration());
            modelBuilder.ApplyConfiguration(new ProductoJVMConfiguration());
            modelBuilder.ApplyConfiguration(new OrdenClienteJVMConfiguration());
            modelBuilder.ApplyConfiguration(new OrdenEmpleadoJVMConfiguration());

            /////////// JOINVIEWMODELS BLOQUE PARA CREAR LAS VISTAS EN LA BASE DE DATOS ELMINAR ESTE BLOQUE DAÑA CASOS DE USO ESTE BLOQUE DEPENDE DE LA MIGRACION CON EL MISMO NOMBRE SI SE ELIMINA LA MIGRACION ESTO NO SIRVE


            //          EntityConfiguration(modelBuilder);/////NO USAR PODRIA SER CONTRADICTORIO   Notiene caso ya que se esta usando doirectamente ya, pero por conocimiento
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Producto_Orden> Productos_Ordenes { get; set; } 
        public DbSet<Producto_Sucursal> Productos_Sucursales { get; set; }


        /////////// JOINVIEWMODELS BLOQUE PARA CREAR LAS VISTAS EN LA BASE DE DATOS ELMINAR ESTE BLOQUE DAÑA CASOS DE USO ESTE BLOQUE DEPENDE DE LA MIGRACION CON EL MISMO NOMBRE SI SE ELIMINA LA MIGRACION ESTO NO SIRVE

        public DbSet<ClienteJVM> ClientesJVM_View { get; set; }
        public DbSet<ProductoJVM> ProductosJVM_View { get; set; }
        public DbSet<OrdenClienteJVM> OrdenesClienteJVM_View { get; set; }
        public DbSet<OrdenEmpleadoJVM> OrdenesEmpleadoJVM_View { get; set; }

        /////////// JOINVIEWMODELS BLOQUE PARA CREAR LAS VISTAS EN LA BASE DE DATOS ELMINAR ESTE BLOQUE DAÑA CASOS DE USO ESTE BLOQUE DEPENDE DE LA MIGRACION CON EL MISMO NOMBRE SI SE ELIMINA LA MIGRACION ESTO NO SIRVE



        //NO SE ESTAN OCUPANDO NINIGUNA DE LAS CLASE SE PUEDE ELIMINAR LA CARPETA DE CONFIGURACION DE PERSISTENCE Y ESTE METODO PERO POR CONOCIMIENTO DE COMO TAMBIEN SE PUEDE HACER DEJARLO

        /*
        private void EntityConfiguration(ModelBuilder modelbuilder)
        {
            new ClienteConfiguration(modelbuilder.Entity<Cliente>());
            new DireccionConfiguration(modelbuilder.Entity<Direccion>());
            new EmpleadoConfiguration(modelbuilder.Entity<Empleado>());
            new OrdenConfiguration(modelbuilder.Entity<Orden>());
            new ProductoConfiguration(modelbuilder.Entity<Producto>());
            new SucursalConfiguration(modelbuilder.Entity<Sucursal>());
            new TarjetaConfiguration(modelbuilder.Entity<Tarjeta>());
        }
        */


    }
}
