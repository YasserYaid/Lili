using System.ComponentModel.DataAnnotations;

namespace ReposteriaLili_API.Core.Application.DTO
{
    public class AssignDeliveryRequestDTO
    {

        [Required]
        public int IdOrden { get; set; }
        [Required]
        public int IdRepartidor { get; set; }
    }
}