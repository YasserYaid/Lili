using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Dominio
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Nombre { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? ApellidoPaterno { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? ApellidoMaterno { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        public string? Correo { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Telefono { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? UserName { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        public string? Contrasena { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public Tarjeta? Tarjeta { get; set; }

        public Direccion? Direccion {  get; set; }

    }
}
