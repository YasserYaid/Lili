using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReposteriaLili_API.Core.Application.DTO.ProductosDTOs
{
    public class ProductosOrderRequestDTO
    {
        [Required]
        public int IdProducto { get; set; }
        [Required]
        public int IdSucursal { get; set; }
        [Required]
        public int CantidadOrdenada { get; set; }
        [Required]
        public int CantidadSucursal { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public decimal? Precio { get; set; }
        [Required]
        public int? CantidadDisponible { get; set; }
        [Required]
        public string? Categoria { get; set; }
        [Required]
        public string? Descripcion { get; set; }
        [Required]
        public string? ImagenUrl { get; set; }
        [Required]
        public string? ImagenCodigoBarrasUrl { get; set; }
        [Required]
        public string? CodigoBarras { get; set; }
        [Required]
        public DateTime? FechaCaducidad { get; set; }
    }
}
