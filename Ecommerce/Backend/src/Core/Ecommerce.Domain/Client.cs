using Ecommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain
{
    public class Client : BaseDomainModel
    {
        public Usuario? Usuario { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public virtual ICollection<ShoppingCart>? ShoppingCars { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Address>? Addresses { get; set; }
        public string? idAspNetUser {  get; set; }
    }
}
