using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Application.Features.Roles.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Roles.Queries.GetRolesList;


public class GetRolesListQuery :  IRequest<IReadOnlyList<RolVm>>
{
}