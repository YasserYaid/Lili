using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Application.DTO.ProductosDTOs
{
    public class ProductoCreateDTO
    {
        [Required]        
        public string? Nombre { get; set; }
        
        [Required]        
        public decimal? Precio { get; set; }
        
        public int? CantidadDisponible { get; set; }
        
        [Required]
        public string? Categoria { get; set; }
        
        [Required]        
        public string? Descripcion { get; set; }
        
        public string? ImagenUrl { get; set; }
        
        public string? ImagenCodigoBarrasUrl { get; set; }
        
        public string? CodigoBarras { get; set; }
        
        public DateTime? FechaCaducidad { get; set; }
        
        [Required]
        public int IdSucursal { get; set; }

        [Required]//USAR ESTA - LA CANTIDAD DISPONIBLE DESPUES ASIGNARA ESTA
        public int Cantidad { get; set; }

    }
}
