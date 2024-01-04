using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReposteriaLili_API.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    ApellidoPaterno = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    ApellidoMaterno = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Correo = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Telefono = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    UserName = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Contrasena = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Precio = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: true),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: true),
                    Categoria = table.Column<string>(type: "NVARCHAR(300)", nullable: true),
                    Descripcion = table.Column<string>(type: "NVARCHAR(3000)", nullable: true),
                    ImagenUrl = table.Column<string>(type: "NVARCHAR(3000)", nullable: true),
                    ImagenCodigoBarrasUrl = table.Column<string>(type: "NVARCHAR(3000)", nullable: true),
                    CodigoBarras = table.Column<string>(type: "NVARCHAR(300)", nullable: true),
                    FechaCaducidad = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProducto);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    IdSucursal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Ciudad = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Municipio = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Calle = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: true),
                    Colonia = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    CodigoPostal = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    Latitud = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    Longitud = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    DiaInicial = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    DiaFinal = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    HoraInicial = table.Column<TimeSpan>(type: "time", nullable: true),
                    HoraFinal = table.Column<TimeSpan>(type: "time", nullable: true),
                    NombreComercial = table.Column<string>(type: "NVARCHAR(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.IdSucursal);
                });

            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    IdDireccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Ciudad = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Municipio = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Calle = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    NumeroExterior = table.Column<int>(type: "int", nullable: true),
                    NumeroInterior = table.Column<int>(type: "int", nullable: true),
                    Colonia = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    CodigoPostal = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    Latitud = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    Longitud = table.Column<string>(type: "NVARCHAR(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.IdDireccion);
                    table.ForeignKey(
                        name: "FK_Direccion_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarjetas",
                columns: table => new
                {
                    IdTarjeta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    CodigoCVV = table.Column<int>(type: "int", nullable: true),
                    Emisor = table.Column<string>(type: "NVARCHAR(300)", nullable: true),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NombreTitular = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    NumeroTarjeta = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    TipoTarjeta = table.Column<string>(type: "NVARCHAR(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjetas", x => x.IdTarjeta);
                    table.ForeignKey(
                        name: "FK_Tarjetas_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursal = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    ApellidoPaterno = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    ApellidoMaterno = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Correo = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Telefono = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    UserName = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Cargo = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Contrasena = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.IdEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleados_Sucursales_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "IdSucursal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Productos_Sucursales",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    IdSucursal = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos_Sucursales", x => new { x.IdProducto, x.IdSucursal });
                    table.ForeignKey(
                        name: "FK_Productos_Sucursales_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productos_Sucursales_Sucursales_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "IdSucursal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    IdOrden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDireccionCliente = table.Column<int>(type: "int", nullable: false),
                    IdDireccionSucursal = table.Column<int>(type: "int", nullable: false),
                    IdEmpleadoRepartidor = table.Column<int>(type: "int", nullable: true),
                    IdTarjeta = table.Column<int>(type: "int", nullable: false),
                    DescripcionIncidenteCliente = table.Column<string>(type: "NVARCHAR(3000)", nullable: true),
                    DescripcionIncidenteRepartidor = table.Column<string>(type: "NVARCHAR(3000)", nullable: true),
                    ImagenIncidenteCliente = table.Column<string>(type: "NVARCHAR(3000)", nullable: true),
                    ImagenIncidenteRepartidor = table.Column<string>(type: "NVARCHAR(3000)", nullable: true),
                    EstadoPedido = table.Column<string>(type: "NVARCHAR(30)", nullable: true),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImporteTotalOrden = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: true),
                    NumeroFolio = table.Column<int>(type: "int", nullable: true),
                    TiempoEstimadoEntrega = table.Column<TimeSpan>(type: "time", nullable: true),
                    TiempoTardoEntrega = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.IdOrden);
                    table.ForeignKey(
                        name: "FK_Ordenes_Direccion_IdDireccionCliente",
                        column: x => x.IdDireccionCliente,
                        principalTable: "Direccion",
                        principalColumn: "IdDireccion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ordenes_Empleados_IdEmpleadoRepartidor",
                        column: x => x.IdEmpleadoRepartidor,
                        principalTable: "Empleados",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ordenes_Sucursales_IdDireccionSucursal",
                        column: x => x.IdDireccionSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "IdSucursal");
                    table.ForeignKey(
                        name: "FK_Ordenes_Tarjetas_IdTarjeta",
                        column: x => x.IdTarjeta,
                        principalTable: "Tarjetas",
                        principalColumn: "IdTarjeta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Productos_Ordenes",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioParcial = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos_Ordenes", x => new { x.IdOrden, x.IdProducto });
                    table.ForeignKey(
                        name: "FK_Productos_Ordenes_Ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "Ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productos_Ordenes_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Correo",
                table: "Clientes",
                column: "Correo",
                unique: true,
                filter: "[Correo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Telefono",
                table: "Clientes",
                column: "Telefono",
                unique: true,
                filter: "[Telefono] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UserName",
                table: "Clientes",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Direccion_IdCliente",
                table: "Direccion",
                column: "IdCliente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Correo",
                table: "Empleados",
                column: "Correo",
                unique: true,
                filter: "[Correo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdSucursal",
                table: "Empleados",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Telefono",
                table: "Empleados",
                column: "Telefono",
                unique: true,
                filter: "[Telefono] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_UserName",
                table: "Empleados",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdDireccionCliente",
                table: "Ordenes",
                column: "IdDireccionCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdDireccionSucursal",
                table: "Ordenes",
                column: "IdDireccionSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdEmpleadoRepartidor",
                table: "Ordenes",
                column: "IdEmpleadoRepartidor");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdTarjeta",
                table: "Ordenes",
                column: "IdTarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ImagenIncidenteCliente",
                table: "Ordenes",
                column: "ImagenIncidenteCliente",
                unique: true,
                filter: "[ImagenIncidenteCliente] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ImagenIncidenteRepartidor",
                table: "Ordenes",
                column: "ImagenIncidenteRepartidor",
                unique: true,
                filter: "[ImagenIncidenteRepartidor] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_NumeroFolio",
                table: "Ordenes",
                column: "NumeroFolio",
                unique: true,
                filter: "[NumeroFolio] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CodigoBarras",
                table: "Productos",
                column: "CodigoBarras",
                unique: true,
                filter: "[CodigoBarras] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ImagenCodigoBarrasUrl",
                table: "Productos",
                column: "ImagenCodigoBarrasUrl",
                unique: true,
                filter: "[ImagenCodigoBarrasUrl] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ImagenUrl",
                table: "Productos",
                column: "ImagenUrl",
                unique: true,
                filter: "[ImagenUrl] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Ordenes_IdProducto",
                table: "Productos_Ordenes",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Sucursales_IdSucursal",
                table: "Productos_Sucursales",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursales_NombreComercial",
                table: "Sucursales",
                column: "NombreComercial",
                unique: true,
                filter: "[NombreComercial] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjetas_IdCliente",
                table: "Tarjetas",
                column: "IdCliente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarjetas_NumeroTarjeta",
                table: "Tarjetas",
                column: "NumeroTarjeta",
                unique: true,
                filter: "[NumeroTarjeta] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos_Ordenes");

            migrationBuilder.DropTable(
                name: "Productos_Sucursales");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Direccion");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Tarjetas");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
