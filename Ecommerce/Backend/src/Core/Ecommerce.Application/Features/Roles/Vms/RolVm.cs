using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Roles.Vms
{
    public class RolVm
    {
        public int id { get; set; }
        public string? type { get; set; }

        public RolVm(int id, string type) 
        { 
            this.id = id;
            this.type = type;
        }
    }
}
