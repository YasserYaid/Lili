using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReposteriaLili_API.Migrations
{
    /// <inheritdoc />
    public partial class JoinViewModelsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW IniciarSesionClienteJVM
            AS
                SELECT	Clientes.IdCliente, Clientes.Nombre, Clientes.ApellidoPaterno, Clientes.ApellidoMaterno, Clientes.Correo, 
		                Clientes.Telefono, Clientes.UserName, Clientes.Contrasena, Clientes.FechaNacimiento,
		                Direccion.IdDireccion, Direccion.Estado, Direccion.Ciudad, Direccion.Municipio, 
		                Direccion.Calle, Direccion.NumeroExterior, Direccion.NumeroInterior, Direccion.Colonia, 
		                Direccion.CodigoPostal, Direccion.Latitud, Direccion.Longitud,
		                Tarjetas.IdTarjeta, Tarjetas.CodigoCVV, Tarjetas.Emisor, Tarjetas.FechaVencimiento,
		                Tarjetas.NombreTitular, Tarjetas.NumeroTarjeta, Tarjetas.TipoTarjeta
                FROM	dbo.Clientes
		                LEFT JOIN dbo.Direccion ON dbo.Direccion.IdCliente = dbo.Clientes.IdCliente
		                LEFT JOIN dbo.Tarjetas ON dbo.Tarjetas.IdCliente = dbo.Clientes.IdCliente
            ");

            migrationBuilder.Sql(@"CREATE VIEW ConsultarProductosJVM
            AS
                SELECT	Productos.IdProducto, Productos.Nombre, Productos.Precio, Productos.CantidadDisponible, Productos.Categoria, 
		                Productos.Descripcion, Productos.ImagenUrl, Productos.ImagenCodigoBarrasUrl, Productos.CodigoBarras,
		                Productos.FechaCaducidad,
		                Productos_Sucursales.Cantidad,
		                Sucursales.IdSucursal, Sucursales.Estado, Sucursales.Ciudad, Sucursales.Municipio, Sucursales.Calle,
		                Sucursales.Numero, Sucursales.Colonia, Sucursales.CodigoPostal, Sucursales.Latitud, Sucursales.Longitud,
		                Sucursales.DiaInicial, Sucursales.DiaFinal, Sucursales.HoraInicial, Sucursales.HoraFinal,
		                Sucursales.NombreComercial
                FROM	dbo.Productos
		                INNER JOIN dbo.Productos_Sucursales ON dbo.Productos_Sucursales.IdProducto = dbo.Productos.IdProducto
		                INNER JOIN dbo.Sucursales ON dbo.Sucursales.IdSucursal = dbo.Productos_Sucursales.IdSucursal
            ");

            migrationBuilder.Sql(@"CREATE VIEW ConsultarOrdenesClientesJVM
            AS
				SELECT	Ordenes.IdOrden, Ordenes.DescripcionIncidenteCliente, Ordenes.DescripcionIncidenteRepartidor, Ordenes.ImagenIncidenteCliente, 
						Ordenes.ImagenIncidenteRepartidor, Ordenes.EstadoPedido, Ordenes.FechaEntrega, Ordenes.FechaSolicitud, 
						Ordenes.ImporteTotalOrden, Ordenes.NumeroFolio, Ordenes.TiempoEstimadoEntrega, Ordenes.TiempoTardoEntrega,
						Productos_Ordenes.Cantidad AS CantidadOrdenada, Productos_Ordenes.PrecioParcial,
						Productos.IdProducto, Productos.Nombre AS NombreProducto, Productos.Precio, Productos.CantidadDisponible AS CantidadDisponibleTotal, Productos.Categoria,
						Productos.Descripcion, Productos.ImagenUrl, Productos.ImagenCodigoBarrasUrl, Productos.CodigoBarras, Productos.FechaCaducidad,
						Productos_Sucursales.Cantidad AS CantidadSucursal,
						Sucursales.IdSucursal, Sucursales.Estado AS EstadoSucursal, Sucursales.Ciudad AS CiudadSucursal, Sucursales.Municipio AS MunicipioSucursal, 
						Sucursales.Calle AS CalleSucursal, Sucursales.Numero AS NumeroCalleSucursal, Sucursales.Colonia AS ColoniaSucursal, 
						Sucursales.CodigoPostal AS CodigoPostalSucural, Sucursales.Latitud AS LatitudSucursal, Sucursales.Longitud AS LongitudSucursal, 
						Sucursales.DiaInicial, Sucursales.DiaFinal, Sucursales.HoraInicial, Sucursales.HoraFinal, Sucursales.NombreComercial,
						Empleados.IdEmpleado, Empleados.Nombre AS NombreEmpleado, Empleados.ApellidoPaterno, Empleados.ApellidoMaterno,
						Empleados.Correo, Empleados.Telefono, Empleados.Cargo,		
						Direccion.IdDireccion, Direccion.Estado, Direccion.Ciudad, Direccion.Municipio, Direccion.Calle,
						Direccion.NumeroExterior, Direccion.NumeroInterior, Direccion.Colonia, Direccion.CodigoPostal, Direccion.Latitud, Direccion.Longitud,
						Tarjetas.IdTarjeta, Tarjetas.IdCliente, Tarjetas.CodigoCVV, Tarjetas.Emisor, Tarjetas.FechaVencimiento, Tarjetas.NombreTitular, 
						Tarjetas.NumeroTarjeta, Tarjetas.TipoTarjeta
				FROM	dbo.Ordenes
						INNER JOIN dbo.Productos_Ordenes ON dbo.Productos_Ordenes.IdOrden = dbo.Ordenes.IdOrden
						INNER JOIN dbo.Productos ON dbo.Productos.IdProducto = dbo.Productos_Ordenes.IdProducto
						INNER JOIN dbo.Tarjetas ON dbo.Tarjetas.IdTarjeta = dbo.Ordenes.IdTarjeta
						INNER JOIN dbo.Direccion ON dbo.Direccion.IdDireccion = dbo.Ordenes.IdDireccionCliente
						INNER JOIN dbo.Sucursales ON dbo.Sucursales.IdSucursal = dbo.Ordenes.IdDireccionSucursal
						INNER JOIN dbo.Productos_Sucursales ON dbo.Productos_Sucursales.IdProducto = dbo.Productos.IdProducto
						LEFT  JOIN dbo.Empleados ON dbo.Empleados.IdEmpleado = dbo.Ordenes.IdEmpleadoRepartidor
            ");

            migrationBuilder.Sql(@"CREATE VIEW ConsultarOrdenesEmpleadosJVM
            AS
				SELECT	Ordenes.IdOrden, Ordenes.DescripcionIncidenteCliente, Ordenes.DescripcionIncidenteRepartidor, Ordenes.ImagenIncidenteCliente, 
						Ordenes.ImagenIncidenteRepartidor, Ordenes.EstadoPedido, Ordenes.FechaEntrega, Ordenes.FechaSolicitud, 
						Ordenes.ImporteTotalOrden, Ordenes.NumeroFolio, Ordenes.TiempoEstimadoEntrega, Ordenes.TiempoTardoEntrega,
						Sucursales.IdSucursal, Sucursales.Estado AS EstadoSucursal, Sucursales.Ciudad AS CiudadSucursal, Sucursales.Municipio AS MunicipioSucursal, 
						Sucursales.Calle AS CalleSucursal, Sucursales.Numero AS NumeroCalleSucursal, Sucursales.Colonia AS ColoniaSucursal, 
						Sucursales.CodigoPostal AS CodigoPostalSucural, Sucursales.Latitud AS LatitudSucursal, Sucursales.Longitud AS LongitudSucursal, 
						Sucursales.DiaInicial, Sucursales.DiaFinal, Sucursales.HoraInicial, Sucursales.HoraFinal, Sucursales.NombreComercial,
						Empleados.IdEmpleado, Empleados.Nombre AS NombreEmpleado, Empleados.ApellidoPaterno, Empleados.ApellidoMaterno,
						Empleados.Correo, Empleados.Telefono, Empleados.Cargo,			
						Direccion.IdDireccion, Direccion.Estado, Direccion.Ciudad, Direccion.Municipio, Direccion.Calle,
						Direccion.NumeroExterior, Direccion.NumeroInterior, Direccion.Colonia, Direccion.CodigoPostal, Direccion.Latitud, Direccion.Longitud
				FROM	dbo.Ordenes
						INNER JOIN dbo.Direccion ON dbo.Direccion.IdDireccion = dbo.Ordenes.IdDireccionCliente
						INNER JOIN dbo.Sucursales ON dbo.Sucursales.IdSucursal = dbo.Ordenes.IdDireccionSucursal
						LEFT  JOIN dbo.Empleados ON dbo.Empleados.IdEmpleado = dbo.Ordenes.IdEmpleadoRepartidor
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IniciarSesionClienteJVM");
            migrationBuilder.Sql("DROP VIEW ConsultarProductosJVM");
            migrationBuilder.Sql("DROP VIEW ConsultarOrdenesClientesJVM");
            migrationBuilder.Sql("DROP VIEW ConsultarOrdenesEmpleadosJVM");
        }
    }
}
