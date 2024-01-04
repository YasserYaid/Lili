using AutoMapper;
using Ecommerce.Application.Features.Countries.Queries.GetCountryList;
using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Application.Features.CountryStates.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.CountryStates.Queries.GetStatesList
{
    public class GetStatesListQueryHandler : IRequestHandler<GetStatesListQuery, IReadOnlyList<CountryStatesVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStatesListQueryHandler( IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CountryStatesVm>> Handle(GetStatesListQuery request, CancellationToken cancellationToken)
        {

            var states = await _unitOfWork.Repository<State>().GetAllAsync();


            return _mapper.Map<IReadOnlyList<CountryStatesVm>>(states);
        }
    }
}
