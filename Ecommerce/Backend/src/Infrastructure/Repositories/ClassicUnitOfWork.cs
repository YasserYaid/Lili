using Ecommerce.Application.Persistence;
using Ecommerce.Application.Persistence.EspecificClassicRepository;
using Ecommerce.Infrastructure.EspecificClassicRepositories;
using Ecommerce.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ClassicUnitOfWork : IClassicUnitOfWork
    {
        private readonly EcommerceDbContext _context;
        public IBranchClassicRepository branchClassicRepository { get; private set; }
        public IEmployeeClassicRepository employeeClassicRepository { get; private set; }
        public IProductClassicRepository productClassicRepository { get; private set; }

        public ClassicUnitOfWork(EcommerceDbContext context)
        {
            _context = context;
            branchClassicRepository = new BranchClassicRepository(_context);
            employeeClassicRepository = new EmployeeClassicRepository(_context);
            productClassicRepository = new ProductClassicRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
