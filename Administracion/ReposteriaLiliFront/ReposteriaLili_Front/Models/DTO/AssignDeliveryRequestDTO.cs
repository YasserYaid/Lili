using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_Front.Models.DTO
{
    public class AssignDeliveryRequestDTO
    {

        [Required]
        public int IdOrden { get; set; }
        [Required]
        public int IdRepartidor { get; set; }
    }
}