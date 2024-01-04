using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_Front.Models.DTO.ProductosDTOs
{
    public class ProductoResponseDTO
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }
        public int? CantidadDisponible { get; set; }
        public string? Categoria { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenUrl { get; set; }
        public string? ImagenCodigoBarrasUrl { get; set; }
        public string? CodigoBarras { get; set; }
        public DateTime? FechaCaducidad { get; set; }
    }
}
