namespace ReposteriaLili_Front.Models.DTO.ClientesDTOs
{
    public class ClienteResponseDTO
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
    }
}
