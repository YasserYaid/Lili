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
    public class BranchClassicRepository : ClassicRepositoryBase<Branch>, IBranchClassicRepository
    {
        public BranchClassicRepository(EcommerceDbContext context) : base(context)
        {            
        }
        ///Aqui se implementarian los metodos personalizados ver el video de yotube de mosh
        public EcommerceDbContext EcommerceDbContext
        {
            get { return _context as EcommerceDbContext; }
        }

    }
}
