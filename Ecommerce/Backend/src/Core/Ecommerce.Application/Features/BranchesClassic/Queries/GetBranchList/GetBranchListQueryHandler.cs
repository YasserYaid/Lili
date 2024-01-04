using AutoMapper;
using Ecommerce.Application.Features.Categories.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.BranchesClassic.Queries.GetBranchList
{
    public class GetBranchListQueryHandler : IRequestHandler<GetBranchListQuery, IReadOnlyList<BranchVM>>
    {
        private readonly IClassicUnitOfWork _classicUnitOfWork;
        private readonly IMapper _mapper;

        public GetBranchListQueryHandler(IClassicUnitOfWork classicUnitOfWork, IMapper mapper)
        {
            this._classicUnitOfWork = classicUnitOfWork;
            this._mapper = mapper;
        }

        public async Task<IReadOnlyList<BranchVM>> Handle(GetBranchListQuery request, CancellationToken cancellationToken)
        {
            var branches = await _classicUnitOfWork.branchClassicRepository.GetAllAsync();
            return _mapper.Map<IReadOnlyList<BranchVM>>(branches);
        }
    }
}
