using Ecommerce.Application.Features.Countries.Queries.GetCountryList;
using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Application.Features.CountryStates.Queries.GetStatesList;
using Ecommerce.Application.Features.CountryStates.Vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountryStateController : ControllerBase
    {
        private IMediator _mediator;

        public CountryStateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetCountryStates")]
        [ProducesResponseType(typeof(IReadOnlyList<CountryStatesVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CountryStatesVm>>> GetCountryStates()
        {
            var query = new GetStatesListQuery();
            return Ok(await _mediator.Send(query));
        }
    }
}
