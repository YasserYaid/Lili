using MediatR;
using ReposteriaLili_API.Core.Application.DTO.SucursalesDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.SucursalesCQRS.Commands.CreateBranch
{
    public record CreateBranchCommand(SucursalCreateDTO CreateSucursalDTO) : IRequest<DatabaseResponse>;
}
