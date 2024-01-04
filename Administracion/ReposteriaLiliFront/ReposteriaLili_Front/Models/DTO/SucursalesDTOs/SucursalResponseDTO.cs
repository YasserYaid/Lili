//CLASE PARA TRANSFERIR INFORMACION CON GET
namespace ReposteriaLili_Front.Models.DTO.SucursalesDTOs
{
    public class SucursalResponseDTO
    {
        public int IdSucursal { get; set; }
        public string? Estado { get; set; }
        public string? Ciudad { get; set; }
        public string? Municipio { get; set; }
        public string? Calle { get; set; }
        public int? Numero { get; set; }
        public string? Colonia { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? DiaInicial { get; set; }
        public string? DiaFinal { get; set; }
        public TimeSpan? HoraInicial { get; set; }
        public TimeSpan? HoraFinal { get; set; }
        public string? NombreComercial { get; set; }
    }
}
