namespace ReposteriaLili_Front.Models.DTO.TarjetasDTOs
{
    public class TarjetaResponseDTO
    {
        public int IdTarjeta { get; set; }
        public int? CodigoCVV { get; set; }
        public string? Emisor { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string? NombreTitular { get; set; }
        public string? NumeroTarjeta { get; set; }
        public string? TipoTarjeta { get; set; }
    }
}
