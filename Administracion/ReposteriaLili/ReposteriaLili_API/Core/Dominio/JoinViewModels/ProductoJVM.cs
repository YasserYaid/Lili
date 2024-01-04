using System.ComponentModel.DataAnnotations.Schema;

namespace ReposteriaLili_API.Core.Dominio.JoinViewModels
{
    public class ProductoJVM
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal? Precio { get; set; }
        public int? CantidadDisponible { get; set; }
        public string? Categoria { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenUrl { get; set; }
        public string? ImagenCodigoBarrasUrl { get; set; }
        public string? CodigoBarras { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public int Cantidad { get; set; }
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
