using AutoMapper;
using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Application.Features.Roles.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Roles.Queries.GetRolesList;

public class GetRolesListQueryHandler : IRequestHandler<GetRolesListQuery, IReadOnlyList<RolVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRolesListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<RolVm>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
    {
        var rolVm1 = new RolVm(1, "ADMINISTRADOR");
        var rolVm2 = new RolVm(2, "REPARTIDOR");
        var rolVm3 = new RolVm(3, "GERENTE-VENTAS");
        IReadOnlyList<RolVm> roles = new List<RolVm>
        {
            rolVm1,
            rolVm2,
            rolVm3
        };
        return roles;
    }
}