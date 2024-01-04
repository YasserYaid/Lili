namespace ReposteriaLili_Front.Models.DTO.DireccionesDTOs
{
    public class DireccionResponseDTO
    {
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
