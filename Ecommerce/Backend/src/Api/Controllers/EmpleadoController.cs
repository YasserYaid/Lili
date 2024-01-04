using System.Net;
using Ecommerce.Application.Features.Auths.Users.Commands.RegisterEmployee;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Features.Roles.Queries.GetRolesList;
using Ecommerce.Application.Features.Roles.Vms;
using Ecommerce.Infrastructure.ImageFirebase;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmpleadoController : ControllerBase
{
    private IMediator _mediator;
    private ManageImageFirebaseService _manageImageFirebaseService;

    public EmpleadoController(IMediator mediator)
    {
        _mediator = mediator;
        _manageImageFirebaseService = new ManageImageFirebaseService();
    }

    [HttpPost("registerEmployee", Name = "RegisterEmployee")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> RegisterEmployee([FromForm] RegisterEmployeeCommand request)
    {
        if (request.foto is not null)
        {
            Console.WriteLine("EL TIPO DE EMPLEADO SELECCIONADO ES : " + request.cargo);
            var streamImage = request.foto.OpenReadStream();
            var name = request.foto.Name + Guid.NewGuid().ToString();
            var resultImage = await _manageImageFirebaseService.UploadImage(streamImage, name, "PROFILE");

            request.fotoId = "a2";
            request.fotoUrl = resultImage;
        }

        return Ok(await _mediator.Send(request));
    }
    [AllowAnonymous]
    [HttpGet("getEmployeeTypes", Name = "GetEmployeeTypes")]
    [ProducesResponseType(typeof(IReadOnlyList<RolVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<RolVm>>> GetEmployeeTypes()
    {
        var query = new GetRolesListQuery();
        return Ok(await _mediator.Send(query));
    }
}
