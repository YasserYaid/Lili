using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Application.Features.CountryStates.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.CountryStates.Queries.GetStatesList
{
    public class GetStatesListQuery : IRequest<IReadOnlyList<CountryStatesVm>>
    {
    }
}
