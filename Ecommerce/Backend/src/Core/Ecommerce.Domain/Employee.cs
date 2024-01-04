using Ecommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain
{
    public class Employee : BaseDomainModel
    {
        public Usuario? Usuario { get; set; }
        public string? Cargo {  get; set; }
        public DateTime? FechaIngreso { get; set; }
        public Branch? Branch { get; set; }
        [Column("BranchId")]
        public int? BranchId { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
        public string? idAspNetUser { get; set; }

    }
}
