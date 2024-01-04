using AutoMapper;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.BranchesClassic.Commands
{
    public class RegisterBranchCommandHandler : IRequestHandler<RegisterBranchCommand, BranchVM>
    {
        private readonly IClassicUnitOfWork _classicUnitOfWork;
        private readonly IMapper _mapper;

        public RegisterBranchCommandHandler(IClassicUnitOfWork classicUnitOfWork, IMapper mapper)
        {
            this._classicUnitOfWork = classicUnitOfWork;
            this._mapper = mapper;
        }

        public async Task<BranchVM> Handle(RegisterBranchCommand request, CancellationToken cancellationToken)
        {
            var branchToRegister = _mapper.Map<Branch>(request);
            var branchRegistered = await _classicUnitOfWork.branchClassicRepository.AddAsync(branchToRegister);
            return _mapper.Map<BranchVM>(branchRegistered);
        }
    }
}

