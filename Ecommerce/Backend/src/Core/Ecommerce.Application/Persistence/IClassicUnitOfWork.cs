using Ecommerce.Application.Persistence.EspecificClassicRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Persistence
{
    public interface IClassicUnitOfWork : IDisposable
    {
        IBranchClassicRepository branchClassicRepository { get; }
        IEmployeeClassicRepository employeeClassicRepository { get; }
        IProductClassicRepository productClassicRepository { get; }

        int Complete();

    }
}
