using Ecommerce.Application.Persistence.EspecificClassicRepository;
using Ecommerce.Domain;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.EspecificClassicRepositories
{
    public class EmployeeClassicRepository : ClassicRepositoryBase<Employee>, IEmployeeClassicRepository
    {
        public EmployeeClassicRepository(EcommerceDbContext context) : base(context)
        {
        }

        ///Aqui va los contratos de los metodos especificos personalizados
        public EcommerceDbContext EcommerceDbContext
        {
            get { return _context as EcommerceDbContext; }
        }
    }
}
