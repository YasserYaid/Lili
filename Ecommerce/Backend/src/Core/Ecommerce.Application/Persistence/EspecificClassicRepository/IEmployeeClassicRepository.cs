using Ecommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Persistence.EspecificClassicRepository
{
    public interface IEmployeeClassicRepository : IClassicRepository<Employee>
    {
        ///Aqui va los contratos de los metodos especificos personalizados
    }
}
