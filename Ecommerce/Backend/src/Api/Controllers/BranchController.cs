using Ecommerce.Application.Features.Auths.Users.Commands.RegisterUser;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Features.BranchesClassic;
using Ecommerce.Application.Features.BranchesClassic.Commands;
using Ecommerce.Application.Features.BranchesClassic.Queries.GetBranchList;
using Ecommerce.Application.Features.Countries.Queries.GetCountryList;
using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Application.Persistence.EspecificClassicRepository;
using Ecommerce.Infrastructure.ImageFirebase;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchClassicRepository _branchrepository;
        private readonly IMediator _mediator;
        public BranchController(IBranchClassicRepository branchClassicRepository, IMediator mediator) 
        { 
            this._branchrepository = branchClassicRepository;
            this._mediator = mediator;
        }


        /////////////////////////////////SE PUEDE HACER DE DOS OPCIONES DIRECTO SIN MEDIATOR Y SIN IR A FEATURES (NO TAN RECOMENDABLE)
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var branches = await _branchrepository.GetAllAsync();
            return this.Ok(branches);
        }

        /////////////////////////////////SE PUEDE HACER DE DOS OPCIONES DIRECTO SIN MEDIATOR Y SIN IR A FEATURES (RECOMENDABLE POR DISEÑO SIMILITUD Y EXPLICACION A LA HORA DE DOCUMENTAR)
        [AllowAnonymous]
        [HttpGet("getBranches", Name = "GetBranches")]
        [ProducesResponseType(typeof(IReadOnlyList<BranchVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<BranchVM>>> GetBranches()
        {
            var query = new GetBranchListQuery();
            return Ok(await _mediator.Send(query));
        }

        [AllowAnonymous]
        [HttpPost("registerBranch", Name = "RegisterBranch")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BranchVM>> Register([FromForm] RegisterBranchCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}
