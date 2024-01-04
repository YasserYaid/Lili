using Ecommerce.Application.Features.Categories.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.BranchesClassic.Queries.GetBranchList
{
    public class GetBranchListQuery : IRequest<IReadOnlyList<BranchVM>>
    {

    }
}
