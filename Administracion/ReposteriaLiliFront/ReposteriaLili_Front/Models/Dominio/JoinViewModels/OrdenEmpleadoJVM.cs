﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ReposteriaLili_Front.Models.Dominio.JoinViewModels
{
    public class OrdenEmpleadoJVM
    {
        public int IdOrden { get; set; }
        public string? DescripcionIncidenteCliente { get; set; }
        public string? DescripcionIncidenteRepartidor { get; set; }
        public string? ImagenIncidenteCliente { get; set; }
        public string? ImagenIncidenteRepartidor { get; set; }
        public string? EstadoPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal? ImporteTotalOrden { get; set; }
        public int? NumeroFolio { get; set; }
        public TimeSpan? TiempoEstimadoEntrega { get; set; }
        public TimeSpan? TiempoTardoEntrega { get; set; }
        public int IdSucursal { get; set; }
        public string? EstadoSucursal { get; set; }
        public string? CiudadSucursal { get; set; }
        public string? MunicipioSucursal { get; set; }
        public string? CalleSucursal { get; set; }
        public int? NumeroCalleSucursal { get; set; }
        public string? ColoniaSucursal { get; set; }
        public string? CodigoPostalSucural { get; set; }
        public string? LatitudSucursal { get; set; }
        public string? LongitudSucursal { get; set; }
        public string? DiaInicial { get; set; }
        public string? DiaFinal { get; set; }
        public TimeSpan? HoraInicial { get; set; }
        public TimeSpan? HoraFinal { get; set; }
        public string? NombreComercial { get; set; }
        public int IdEmpleado { get; set; }
        public string? NombreEmpleado { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Cargo { get; set; }
        public int IdDireccion { get; set; }
        public string? Estado { get; set; }
        public string? Ciudad { get; set; }
        public string? Municipio { get; set; }
        public string? Calle { get; set; }
        public int? NumeroExterior { get; set; }
        public int? NumeroInterior { get; set; }
        public string? Colonia { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
    }
}
