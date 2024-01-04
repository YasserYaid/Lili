namespace ReposteriaLili_API.Core.Dominio.JoinViewModels
{
    public class ClienteJVM
    {
        public int IdCliente { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? UserName { get; set; }
        public string? Contrasena { get; set; }
        public DateTime? FechaNacimiento { get; set; }
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
        public int IdTarjeta { get; set; }
        public int? CodigoCVV { get; set; }
        public string? Emisor { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string? NombreTitular { get; set; }
        public string? NumeroTarjeta { get; set; }
        public string? TipoTarjeta { get; set; }
    }
}
