using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs
{
    public class OrdenUpdateDTO
    {
        [Required]
        public int IdOrden { get; set; }
        public int? IdDireccionCliente { get; set; }//MAPEAR A MANO TODOS LOS ATRIBUTOS EN EL CLIENTE PORQUE HUBO DETALLES EN LA CONVERSION (NO USAR MAPPER SOLO PARA ESTE DTO) obejto1.idDireccion = objeto.idDireccion;
        public int IdDireccionSucursal { get; set; }
        public int? IdEmpleadoRepartidor { get; set; }
        public int? IdTarjeta { get; set; }
        public string? DescripcionIncidenteCliente { get; set; }
        public string? DescripcionIncidenteRepartidor { get; set; }
        public string? ImagenIncidenteCliente { get; set; }
        public string? ImagenIncidenteRepartidor { get; set; }
        public string? EstadoPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public decimal? ImporteTotalOrden { get; set; }
        public int? NumeroFolio { get; set; }
        public bool EsOrdenEntregada { get; set; } = false;
    }
}
